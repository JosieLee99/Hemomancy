using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("useDelaunayMesh", "maximumTileChangeCount", "extrusionFactor", "density", "isTrigger", "usedByEffector", "usedByComposite", "offset", "sharedMaterial", "layerOverridePriority", "excludeLayers", "includeLayers", "forceSendLayers", "forceReceiveLayers", "contactCaptureLayers", "callbackLayers", "enabled", "name")]
	public class ES3UserType_TilemapCollider2D : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_TilemapCollider2D() : base(typeof(UnityEngine.Tilemaps.TilemapCollider2D)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.Tilemaps.TilemapCollider2D)obj;
			
			writer.WriteProperty("useDelaunayMesh", instance.useDelaunayMesh, ES3Type_bool.Instance);
			writer.WriteProperty("maximumTileChangeCount", instance.maximumTileChangeCount, ES3Type_uint.Instance);
			writer.WriteProperty("extrusionFactor", instance.extrusionFactor, ES3Type_float.Instance);
			writer.WriteProperty("density", instance.density, ES3Type_float.Instance);
			writer.WriteProperty("isTrigger", instance.isTrigger, ES3Type_bool.Instance);
			writer.WriteProperty("usedByEffector", instance.usedByEffector, ES3Type_bool.Instance);
			writer.WriteProperty("usedByComposite", instance.usedByComposite, ES3Type_bool.Instance);
			writer.WriteProperty("offset", instance.offset, ES3Type_Vector2.Instance);
			writer.WritePropertyByRef("sharedMaterial", instance.sharedMaterial);
			writer.WriteProperty("layerOverridePriority", instance.layerOverridePriority, ES3Type_int.Instance);
			writer.WriteProperty("excludeLayers", instance.excludeLayers, ES3Type_LayerMask.Instance);
			writer.WriteProperty("includeLayers", instance.includeLayers, ES3Type_LayerMask.Instance);
			writer.WriteProperty("forceSendLayers", instance.forceSendLayers, ES3Type_LayerMask.Instance);
			writer.WriteProperty("forceReceiveLayers", instance.forceReceiveLayers, ES3Type_LayerMask.Instance);
			writer.WriteProperty("contactCaptureLayers", instance.contactCaptureLayers, ES3Type_LayerMask.Instance);
			writer.WriteProperty("callbackLayers", instance.callbackLayers, ES3Type_LayerMask.Instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.Tilemaps.TilemapCollider2D)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "useDelaunayMesh":
						instance.useDelaunayMesh = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "maximumTileChangeCount":
						instance.maximumTileChangeCount = reader.Read<System.UInt32>(ES3Type_uint.Instance);
						break;
					case "extrusionFactor":
						instance.extrusionFactor = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "density":
						instance.density = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "isTrigger":
						instance.isTrigger = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "usedByEffector":
						instance.usedByEffector = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "usedByComposite":
						instance.usedByComposite = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "offset":
						instance.offset = reader.Read<UnityEngine.Vector2>(ES3Type_Vector2.Instance);
						break;
					case "sharedMaterial":
						instance.sharedMaterial = reader.Read<UnityEngine.PhysicsMaterial2D>(ES3Type_PhysicsMaterial2D.Instance);
						break;
					case "layerOverridePriority":
						instance.layerOverridePriority = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "excludeLayers":
						instance.excludeLayers = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
						break;
					case "includeLayers":
						instance.includeLayers = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
						break;
					case "forceSendLayers":
						instance.forceSendLayers = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
						break;
					case "forceReceiveLayers":
						instance.forceReceiveLayers = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
						break;
					case "contactCaptureLayers":
						instance.contactCaptureLayers = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
						break;
					case "callbackLayers":
						instance.callbackLayers = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
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


	public class ES3UserType_TilemapCollider2DArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_TilemapCollider2DArray() : base(typeof(UnityEngine.Tilemaps.TilemapCollider2D[]), ES3UserType_TilemapCollider2D.Instance)
		{
			Instance = this;
		}
	}
}