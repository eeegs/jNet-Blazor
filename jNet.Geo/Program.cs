using System;
using static jNet.Geo.PGA3D;

namespace jNet.Geo
{
	class Program
	{
		static void Main(string[] args)
		{
			// Elements of the even subalgebra (scalar + bivector + pss) of unit length are motors
			var rot = rotor((float)Math.PI / 2.0f, e1 * e2);

			// The outer product ^ is the MEET. Here we intersect the yz (x=0) and xz (y=0) planes.
			var ax_z = e1 ^ e2;

			// line and plane meet in point. We intersect the line along the z-axis (x=0,y=0) with the xy (z=0) plane.
			var orig = ax_z ^ e3;

			// We can also easily create points and join them into a line using the regressive (vee, &) product.
			var px = point(1, 0, 0);
			var line = orig & px;

			// Lets also create the plane with equation 2x + z - 3 = 0
			var p = plane(2, 0, 1, -3);

			// rotations work on all elements
			var rotated_plane = rot * p * ~rot;
			var rotated_line = rot * line * ~rot;
			var rotated_point = rot * px * ~rot;

			// See the 3D PGA Cheat sheet for a huge collection of useful formulas
			var point_on_plane = (p | px) * p;

			// Some output
			Console.WriteLine("a point       : " + px);
			Console.WriteLine("a line        : " + line);
			Console.WriteLine("a plane       : " + p);
			Console.WriteLine("a rotor       : " + rot);
			Console.WriteLine("rotated line  : " + rotated_line);
			Console.WriteLine("rotated point : " + rotated_point);
			Console.WriteLine("rotated plane : " + rotated_plane);
			Console.WriteLine("point on plane: " + point_on_plane.normalized());
			Console.WriteLine("point on torus: " + point_on_torus(0.0f, 0.0f));

			var p1 = plane(1, 1, 0, 0).normalized();
			var p2 = plane(0, 1, 0, 0).normalized();

			var dot = p1 | p2;
		}
	}
}
