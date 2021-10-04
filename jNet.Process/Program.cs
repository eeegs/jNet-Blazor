// See https://aka.ms/new-console-template for more information

using System.Linq.Expressions;
using System.Reflection;

namespace jNet.Process;

class Program
{
	static void Main(string[] args)
	{
		var eb = new ExpressionBuilder();
		var exp = eb.Process(Tokens);
		var func = exp.Compile();
		var d = Data.Where(func).ToList();
	}

	static Token[] Tokens = new Token[]
	{
				new (TokenType.Var, "Manager"),
				new (TokenType.Const, "false"),
				new (TokenType.BoolOp, "=="),
				new (TokenType.Var, "level"),
				new (TokenType.Const, "3"),
				new (TokenType.BoolOp, "=="),
				new (TokenType.SetOp, "-"),
				new (TokenType.Var, "level"),
				new (TokenType.Const, "5"),
				new (TokenType.BoolOp, "="),
				new (TokenType.SetOp, "+"),
	};

	static Dictionary<string, object?>[] Data = new Dictionary<string, object?>[]
	{
				new()
				{
					["level"]=4,
					["FirstName"]= "Scott",
				},
				new()
				{
					["level"]=4,
					["Manager"] = null
				},
				new()
				{
					["level"]=5,
					["Manager"] = null
				},
				new()
				{
					["level"]=5,
				},
				new()
				{
					["level"]=5,
					["Manager"] = true
				},
				new()
				{
					["level"]=3,
				},
				new()
				{
					["level"]=3,
					["Manager"] = null
				},
				new()
				{
					["level"]=7,
					["Manager"] = null
				},
	};
}

public enum TokenType { Var, Const, BoolOp, SetOp, Func, Bracket, Exp }
//public enum ArgType { None, String, Number, Date, Bool }
public record Token(TokenType Type, string Value/*, ArgType ArgType = ArgType.None*/);
public record ArgToken(string Value) : Token(TokenType.Const, Value);



public class ExpressionBuilder
{
	record TokenExpression(Token Token, Expression? Expression);
	static readonly PropertyInfo DictionaryItem = (PropertyInfo)typeof(IDictionary<string, object?>).GetDefaultMembers().First();
	static readonly MethodInfo DictionaryContainsKey = typeof(IDictionary<string, object?>).GetMethod("ContainsKey")!;
	static readonly MethodInfo FixDecimal = typeof(ExpressionBuilder).GetMethod("Fix", BindingFlags.Static | BindingFlags.NonPublic)!;

	readonly Stack<TokenExpression> _stack = new();
	readonly ParameterExpression _dictionary = Expression.Parameter(typeof(IDictionary<string, object?>), "bag");

	public ExpressionBuilder()
	{
	}

	public Expression<Func<IDictionary<string, object?>, bool>> Process(IEnumerable<Token> tokens)
	{
		foreach (var t in tokens)
		{
			TokenExpression tokenExp = t.Type switch
			{
				TokenType.BoolOp => ProcessFilter(t),
				TokenType.SetOp => ProcessBool(t),
				TokenType.Var => new TokenExpression(t, Expression.Call(_dictionary, DictionaryContainsKey, Expression.Constant(t.Value))),
				_ => new TokenExpression(t, Expression.Empty()),
			};
			_stack.Push(tokenExp);
		}
		var exp = _stack.Pop().Expression!;
		var func = Expression.Lambda<Func<IDictionary<string, object?>, bool>>(exp, _dictionary);
		return func;
	}

	private TokenExpression ProcessBool(Token op)
	{

		var rightExp = MakeBool(_stack.Pop());
		var leftExp = MakeBool(_stack.Pop());

		Expression result = op.Value switch
		{
			"*" => Expression.AndAlso(leftExp, rightExp),
			"+" => Expression.Or(leftExp, rightExp),
			"-" => Expression.AndAlso(leftExp, Expression.Not(rightExp)),
			_ => Expression.Constant(false)
		};
		return new TokenExpression(op, result);
	}

	private Expression MakeBool(TokenExpression token)
	{
		if (token.Expression != null)
		{
			if (token.Expression.Type != typeof(bool))
			{
				throw new ArgumentException($"Operation {token.Token.Value}({token.Expression}) did not return a boolean.");
			}
			return token.Expression;
		}
		return Expression.Call(_dictionary, DictionaryContainsKey, Expression.Constant(token.Token.Value));
	}

	private TokenExpression ProcessFilter(Token op)
	{
		var arg = _stack.Pop().Token;
		var key = _stack.Pop().Token;

		var typedArg = DeterminType(arg.Value);
		var keyExp = Expression.Constant(key.Value);
		var containskey = Expression.Call(_dictionary, DictionaryContainsKey, keyExp);
		var item = Expression.Property(_dictionary, DictionaryItem, keyExp);

		var left = typedArg.Value is decimal ? Expression.Call(FixDecimal, item) : (Expression)Expression.Convert(item, typedArg.Type);
		var right = Expression.Constant(typedArg.Value, typedArg.Type);

		Expression filter = typedArg.Value switch
		{
			decimal or DateTime => op.Value switch
			{
				"==" or "=" => Expression.Equal(left, right),
				"!=" or "<>" => Expression.NotEqual(left, right),
				">" => Expression.GreaterThan(left, right),
				"<" => Expression.LessThan(left, right),
				">=" => Expression.GreaterThanOrEqual(left, right),
				"<=" => Expression.LessThanOrEqual(left, right),
				_ => Expression.Constant(false)
			},
			string or bool => op.Value switch
			{
				"==" or "=" => Expression.Equal(left, right),
				"!=" or "<>" => Expression.NotEqual(left, right),
				_ => Expression.Constant(false)
			},
			_ => Expression.Constant(false)
		};

		var andalso = Expression.AndAlso(containskey, filter);
		return new TokenExpression(op, andalso);
	}

	static (object Value, Type Type) DeterminType(string value)
	{
		if (decimal.TryParse(value, out var r1))
		{
			return (r1, typeof(decimal?));
		}

		if (bool.TryParse(value, out var r2))
		{
			return (r2, typeof(bool?));
		}

		if (DateTime.TryParse(value, out var r3))
		{
			return (r3, typeof(DateTime?));
		}
		return (value, typeof(string));
	}

	// don't delete, called thru reflection
	static decimal? Fix(object value) => value switch
	{
		int x => x,
		double x => (decimal)x,
		float x => (decimal)x,
		short x => x,
		long x => x,
		byte x => x,
		_ => 0,
	};
}

public class Entity
{
	public string? Name { get; set; }
	public Dictionary<string, object> Properties = new();

}
