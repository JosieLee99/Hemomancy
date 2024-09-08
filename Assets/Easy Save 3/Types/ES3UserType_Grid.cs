using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("cellSize", "cellGap", "cellLayout", "cellSwizzle", "enabled", "name")]
	public class ES3UserType_Grid : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Grid() : base(typeof(UnityEngine.Grid)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.Grid)obj;
			
			writer.WriteProperty("cellSize", instance.cellSize, ES3Type_Vector3.Instance);
			writer.WriteProperty("cellGap", instance.cellGap, ES3Type_Vector3.Instance);
			writer.WriteProperty("cellLayout", instance.cellLayout, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.GridLayout.CellLayout)));
			writer.WriteProperty("cellSwizzle", instance.cellSwizzle, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.GridLayout.CellSwizzle)));
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.Grid)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "cellSize":
						instance.cellSize = reader.Read<UnityEngine.Vector3>(ES3Type_Vector3.Instance);
						break;
					case "cellGap":
						instance.cellGap = reader.Read<UnityEngine.Vector3>(ES3Type_Vector3.Instance);
						break;
					case "cellLayout":
						instance.cellLayout = reader.Read<UnityEngine.GridLayout.CellLayout>();
						break;
					case "cellSwizzle":
						instance.cellSwizzle = reader.Read<UnityEngine.GridLayout.CellSwizzle>();
						break;
					case "enabled":
						instance.enabled = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_GridArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_GridArray() : base(typeof(UnityEngine.Grid[]), ES3UserType_Grid.Instance)
		{
			Instance = this;
		}
	}
}