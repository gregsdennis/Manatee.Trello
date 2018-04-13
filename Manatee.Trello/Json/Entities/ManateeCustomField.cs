﻿using System;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeCustomField : IJsonCustomField, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonCustomFieldDefinition Definition { get; set; }
		public string Text { get; set; }
		public double? Number { get; set; }
		public DateTime? Date { get; set; }
		public bool? Checked { get; set; }
		public IJsonCustomDropDownOption Selected { get; set; }
		public CustomFieldType Type { get; set; }
		public bool ValidForMerge { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					Definition = serializer.Deserialize<IJsonCustomFieldDefinition>(obj);
					Selected = obj.Deserialize<IJsonCustomDropDownOption>(serializer, "idValue");
					Type = obj.Deserialize<CustomFieldType?>(serializer, "type") ?? CustomFieldType.Unknown;
					ValidForMerge = true;
					if (Selected != null)
					{
						Type = CustomFieldType.DropDown;
						break;
					}
					var value = obj.TryGetObject("value");
					if (value != null)
					{
						Text = value.TryGetString("text");
						if (Text != null)
						{
							Type = CustomFieldType.Text;
							break;
						}
						var numberString = value.TryGetString("number");
						Number = numberString != null && double.TryParse(numberString, out var number)
							         ? number
							         : (double?) null;
						if (Number != null)
						{
							Type = CustomFieldType.Number;
							break;
						}
						var boolString = value.TryGetString("checked");
						Checked = boolString != null && bool.TryParse(boolString, out var boolean)
							          ? boolean
							          : (bool?) null;
						if (Checked != null)
						{
							Type = CustomFieldType.CheckBox;
							break;
						}
						var dateString = value.TryGetString("date");
						Date = dateString != null && DateTime.TryParse(dateString, out var date)
							       ? date
							       : (DateTime?) null;
						if (Date != null)
						{
							Type = CustomFieldType.DateTime;
							break;
						}
					}
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}

		public JsonValue ToJson(JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}