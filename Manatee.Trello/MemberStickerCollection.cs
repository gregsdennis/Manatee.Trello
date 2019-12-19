﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of <see cref="ISticker"/>s.
	/// </summary>
	public class MemberStickerCollection : ReadOnlyStickerCollection, IMemberStickerCollection
	{
		internal MemberStickerCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) { }

        /// <summary>
        /// Adds a <see cref="ISticker"/> to a <see cref="IMember"/>'s custom sticker set by uploading data.
        /// </summary>
        /// <param name="filePath">The path of the file to attach.</param>
        /// <param name="name">A name for the sticker.</param>
        /// <param name="ct">(Optional) A cancellation token for async processing.</param>
        /// <returns>The <see cref="ISticker"/> generated by Trello.</returns>
        public async Task<ISticker> Add(string filePath, string name, CancellationToken ct = default)
		{
            if (!File.Exists(filePath))  throw new Exception(filePath + " Invalid file path");
            
            var parameters = new Dictionary<string, object> { { RestFile.ParameterKey, new RestFile { FilePath = filePath, FileName = name } } };
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AddAttachment, new Dictionary<string, object> { { "_id", OwnerId } });
			var newData = await JsonRepository.Execute<IJsonSticker>(Auth, endpoint, ct, parameters);

			return new Sticker(newData, OwnerId, Auth);
		}
	}
}