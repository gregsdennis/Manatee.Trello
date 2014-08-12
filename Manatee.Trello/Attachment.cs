﻿/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Attachment.cs
	Namespace:		Manatee.Trello
	Class Name:		Attachment
	Purpose:		Represents an attachment to a card.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents an attachment to a card.
	/// </summary>
	public class Attachment : ICacheable
	{
		private readonly Field<int?> _bytes;
		private readonly Field<DateTime?> _date;
		private readonly Field<bool?> _isUpload;
		private readonly Field<Member> _member;
		private readonly Field<string> _mimeType;
		private readonly Field<string> _name;
		private readonly ReadOnlyAttachmentPreviewCollection _previews;
		private readonly Field<string> _url;
		private readonly AttachmentContext _context;

		/// <summary>
		/// Gets the size of the attachment in bytes.
		/// </summary>
		public int? Bytes { get { return _bytes.Value; } }
		/// <summary>
		/// Gets the date and time the attachment was added to a card.
		/// </summary>
		public DateTime? Date { get { return _date.Value; } }
		/// <summary>
		/// Gets the attachment's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets whether the attachment was uploaded data or attached by URI.
		/// </summary>
		public bool? IsUpload { get { return _isUpload.Value; } }
		/// <summary>
		/// Gets the <see cref="Member"/> who added the attachment.
		/// </summary>
		public Member Member { get { return _member.Value; } }
		/// <summary>
		/// Gets the MIME type of the attachment.
		/// </summary>
		public string MimeType { get { return _mimeType.Value; } }
		/// <summary>
		/// Gets the name of the attachment.
		/// </summary>
		public string Name { get { return _name.Value; } }
		/// <summary>
		/// Gets the collection of previews generated by Trello.
		/// </summary>
		public ReadOnlyAttachmentPreviewCollection Previews { get { return _previews; } }
		/// <summary>
		/// Gets the URI of the attachment.
		/// </summary>
		public string Url { get { return _url.Value; } }

		internal IJsonAttachment Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

#if IOS
		private Action<Attachment, IEnumerable<string>> _updatedInvoker;

		/// <summary>
		/// Raised when data on the attachment is updated.
		/// </summary>
		public event Action<Attachment, IEnumerable<string>> Updated
		{
			add { _updatedInvoker += value; }
			remove { _updatedInvoker -= value; }
		}
#else
		/// <summary>
		/// Raised when data on the attachment is updated.
		/// </summary>
		public event Action<Attachment, IEnumerable<string>> Updated;
#endif

		internal Attachment(IJsonAttachment json, string ownerId)
		{
			Id = json.Id;
			_context = new AttachmentContext(Id, ownerId);
			_context.Synchronized += Synchronized;

			_bytes = new Field<int?>(_context, () => Bytes);
			_date = new Field<DateTime?>(_context, () => Date);
			_member = new Field<Member>(_context, () => Member);
			_isUpload = new Field<bool?>(_context, () => IsUpload);
			_mimeType = new Field<string>(_context, () => MimeType);
			_name = new Field<string>(_context, () => Name);
			_previews = new ReadOnlyAttachmentPreviewCollection(_context);
			_url = new Field<string>(_context, () => Url);

			TrelloConfiguration.Cache.Add(this);

			_context.Merge(json);
		}

		/// <summary>
		/// Deletes the attachment.
		/// </summary>
		/// <remarks>
		/// This cannot be undone.
		/// </remarks>
		public void Delete()
		{
			_context.Delete();
			TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return Name;
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
#if IOS
			var handler = _updatedInvoker;
#else
			var handler = Updated;
#endif
			if (handler != null)
				handler(this, properties);
		}
	}
}