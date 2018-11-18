using UnityEngine;
using System.Collections;

public class DrawingHelper : MonoBehaviour
{
	public static GameObject DrawCube(Vector3 pos, Vector3 size, Color color, Transform parent, bool destroy = false, float duration = 0.02f)
	{
		GameObject myBox = new GameObject("BoxRenderer");
		myBox.transform.SetParent(parent);
		size = size / 2 - new Vector3(0.1f, 0.1f, 0.1f);
		Vector3 a = pos;
		a.x -= size.x;
		a.y += size.y;
		a.z += size.z;

		Vector3 b = pos;
		b.x += size.x;
		b.y += size.y;
		b.z += size.z;

		Vector3 c = pos;
		c.x += size.x;
		c.y += size.y;
		c.z -= size.z;

		Vector3 d = pos;
		d.x -= size.x;
		d.y += size.y;
		d.z -= size.z;

		Vector3 e = pos;
		e.x -= size.x;
		e.y -= size.y;
		e.z += size.z;

		Vector3 f = pos;
		f.x += size.x;
		f.y -= size.y;
		f.z += size.z;

		Vector3 g = pos;
		g.x += size.x;
		g.y -= size.y;
		g.z -= size.z;

		Vector3 h = pos;
		h.x -= size.x;
		h.y -= size.y;
		h.z -= size.z;

		DrawSquare(new Vector3[] { a, b, c, d }, color, myBox.transform, destroy, duration);
		DrawSquare(new Vector3[] { a, b, f, e }, color, myBox.transform, destroy, duration);
		DrawSquare(new Vector3[] { b, c, g, f }, color, myBox.transform, destroy, duration);
		DrawSquare(new Vector3[] { c, d, h, g }, color, myBox.transform, destroy, duration);
		DrawSquare(new Vector3[] { d, a, e, h }, color, myBox.transform, destroy, duration);
		DrawSquare(new Vector3[] { e, f, g, h }, color, myBox.transform, destroy, duration);
		return myBox;
	}

	public static GameObject DrawLine(Vector3 start, Vector3 end, Color color, Transform parent, bool destroy = false, float duration = 0.02f)
	{
		GameObject myLine = new GameObject("LineRenderer");
		myLine.transform.SetParent(parent);
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.startColor = color;
		lr.endColor = color;
		lr.startWidth = 0.05f;
		lr.endWidth = 0.05f;
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
		lr.receiveShadows = false;

		if (destroy)
		{
			Destroy(myLine, duration);
		}
		return myLine;
	}

	public static GameObject DrawSquare(Vector3[] vertices, Color color, Transform parent, bool destroy = false, float duration = 0.02f)
	{
		GameObject mySquare = new GameObject("SquareRenderer");
		mySquare.transform.SetParent(parent);
		mySquare.transform.position = parent.position;
		mySquare.AddComponent<LineRenderer>();
		LineRenderer lr = mySquare.GetComponent<LineRenderer>();
		lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.startColor = color;
		lr.endColor = color;
		lr.startWidth = 0.05f;
		lr.endWidth = 0.05f;
		lr.positionCount = 4;
		lr.numCornerVertices = 4;
		lr.SetPosition(0, vertices[0]);
		lr.SetPosition(1, vertices[1]);
		lr.SetPosition(2, vertices[2]);
		lr.SetPosition(3, vertices[3]);
		lr.loop = true;
		lr.receiveShadows = false;
		if (destroy)
		{
			Destroy(mySquare, duration);
		}
		return mySquare;
	}
}
