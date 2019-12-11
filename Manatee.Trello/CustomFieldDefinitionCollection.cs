﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of custom field definitions.
	/// </summary>
	public class CustomFieldDefinitionCollection : ReadOnlyCustomFieldDefinitionCollection,
	                                               ICustomFieldDefinitionCollection
	{
		internal CustomFieldDefinitionCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		/// <summary>
		/// Adds a new custom field definition to a board.
		/// </summary>
		/// <param name="name">The field's name.</param>
		/// <param name="type">The field's data type.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <param name="options">(Optional) A collection of drop down options.</param>
		/// <returns>The <see cref="ICustomFieldDefinition"/> generated by Trello.</returns>
		public async Task<ICustomFieldDefinition> Add(string name, CustomFieldType type,
		                                              CancellationToken ct = default,
		                                              params IDropDownOption[] options)
		{
			var error = NotNullRule<string>.Instance.Validate(null, name);
			if (error != null)
				throw new ValidationException<string>(name, new[] {error});
			if (type == CustomFieldType.Unknown)
				throw new ValidationException<CustomFieldType>(type, new string[] { });

			var json = TrelloConfiguration.JsonFactory.Create<IJsonCustomFieldDefinition>();
			json.Name = name;
			json.Board = TrelloConfiguration.JsonFactory.Create<IJsonBoard>();
			json.Board.Id = OwnerId;
			json.Type = type;
			json.Display = TrelloConfiguration.JsonFactory.Create<IJsonCustomFieldDisplayInfo>();
			json.Display.CardFront = true;
			if (type == CustomFieldType.DropDown)
			{
				json.Options = options.Select((o, i) =>
					{
						var optionJson = TrelloConfiguration.JsonFactory.Create<IJsonCustomDropDownOption>();
						optionJson.Color = o.Color;
						optionJson.Pos = TrelloConfiguration.JsonFactory.Create<IJsonPosition>();
						optionJson.Pos.Explicit = i * 1024;
						optionJson.Text = o.Text;

						return optionJson;
					}).ToList();
			}

			var endpoint = EndpointFactory.Build(EntityRequestType.CustomFieldDefinition_Write_Create);
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			return new CustomFieldDefinition(newData, Auth);
		}
	}
}