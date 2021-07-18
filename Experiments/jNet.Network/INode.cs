using System;
using System.Collections.Generic;

namespace jNet.Network
{
	public interface IId
	{
		public int Id { get; set; }
	}
	
	public interface IHaveName
	{
		string Name { get; }
	}
	public interface IHaveBag//: IEnumerable<KeyValuePair<string, object?>>
	{
		object? this[string key] { get; set; }
	}
	public interface IHaveInfo: IHaveName, IHaveBag, IId
	{
	}
}
