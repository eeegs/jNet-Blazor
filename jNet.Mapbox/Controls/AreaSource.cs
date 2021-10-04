namespace jNet.Mapbox
{
	public class AreaSource<T> : BaseSource<T, double[][]>
		where T : IHaveId
	{
		protected override string TypeName => "MultiPolygon";
	}
}
