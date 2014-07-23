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
 
	File Name:		Card.cs
	Namespace:		Manatee.Trello
	Class Name:		Card
	Purpose:		Represents a card.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a card.
	/// </summary>
	public class Card : ICanWebhook, IQueryable
	{
		private readonly Field<Board> _board;
		private readonly Field<string> _description;
		private readonly Field<DateTime?> _dueDate;
		private readonly Field<bool?> _isArchived;
		private readonly Field<bool?> _isSubscribed;
		private readonly Field<DateTime?> _lastActivity;
		private readonly Field<List> _list;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly Field<int?> _shortId;
		private readonly Field<string> _shortUrl;
		private readonly Field<string> _url;
		private readonly CardContext _context;

		private bool _deleted;

		/// <summary>
		/// Gets the collection of actions performed on this card.
		/// </summary>
		public ReadOnlyActionCollection Actions { get; private set; }
		/// <summary>
		/// Gets the collection of attachments contained in the card.
		/// </summary>
		public AttachmentCollection Attachments { get; private set; }
		/// <summary>
		/// Gets the badges summarizing the content of the card.
		/// </summary>
		public Badges Badges { get; private set; }
		/// <summary>
		/// Gets the board to which the card belongs.
		/// </summary>
		public Board Board { get { return _board.Value; } }
		/// <summary>
		/// Gets the collection of checklists contained in the card.
		/// </summary>
		public CheckListCollection CheckLists { get; private set; }
		/// <summary>
		/// Gets the collection of comments made on the card.
		/// </summary>
		public CommentCollection Comments { get; private set; }
		/// <summary>
		/// Gets or sets the card's description.
		/// </summary>
		public string Description
		{
			get { return _description.Value; }
			set { _description.Value = value; }
		}
		/// <summary>
		/// Gets or sets the card's due date.
		/// </summary>
		public DateTime? DueDate
		{
			get { return _dueDate.Value; }
			set { _dueDate.Value = value; }
		}
		/// <summary>
		/// Gets the card's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets or sets whether the card is archived.
		/// </summary>
		public bool? IsArchived
		{
			get { return _isArchived.Value; }
			set { _isArchived.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether the current member is subscribed to the card.
		/// </summary>
		public bool? IsSubscribed
		{
			get { return _isSubscribed.Value; }
			set { _isSubscribed.Value = value; }
		}
		/// <summary>
		/// Gets the collection of labels on the card.
		/// </summary>
		public LabelCollection Labels { get; private set; }
		/// <summary>
		/// Gets the most recent date of activity on the card.
		/// </summary>
		public DateTime? LastActivity { get { return _lastActivity.Value; } }
		/// <summary>
		/// Gets or sets the list to the card belongs.
		/// </summary>
		public List List
		{
			get { return _list.Value; }
			set { _list.Value = value; }
		}
		/// <summary>
		/// Gets the collection of members who are assigned to the card.
		/// </summary>
		public MemberCollection Members { get; private set; }
		/// <summary>
		/// Gets or sets the card's name.
		/// </summary>
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		/// <summary>
		/// Gets or sets the card's position.
		/// </summary>
		public Position Position
		{
			get { return _position.Value; }
			set { _position.Value = value; }
		}
		/// <summary>
		/// Gets the card's short ID.
		/// </summary>
		public int? ShortId { get { return _shortId.Value; } }
		/// <summary>
		/// Gets the card's short URL.
		/// </summary>
		/// <remarks>
		/// Because this value does not change, it can be used as a permalink.
		/// </remarks>
		public string ShortUrl { get { return _shortUrl.Value; } }
		/// <summary>
		/// Gets the card's full URL.
		/// </summary>
		/// <remarks>
		/// 
		/// </remarks>
		public string Url { get { return _url.Value; } }

		internal IJsonCard Json { get { return _context.Data; } }

		/// <summary>
		/// Raised when data on the card is updated.
		/// </summary>
		public event Action<Card, IEnumerable<string>> Updated; 

		/// <summary>
		/// Creates a new instance of the <see cref="Card"/> object.
		/// </summary>
		/// <param name="id">The card's ID.</param>
		/// <remarks>
		/// The supplied ID can be either the full or short ID.
		/// </remarks>
		public Card(string id)
			: this(id, true) {}
		internal Card(IJsonCard json, bool cache)
			: this(json.Id, cache)
		{
			_context.Merge(json);
		}
		private Card(string id, bool cache)
		{
			Id = id;
			_context = new CardContext(id);
			_context.Synchronized += Synchronized;

			Actions = new ReadOnlyActionCollection(typeof(Card), id);
			Attachments = new AttachmentCollection(id);
			Badges = new Badges(_context.BadgesContext);
			_board = new Field<Board>(_context, () => Board);
			_board.AddRule(NotNullRule<Board>.Instance);
			CheckLists = new CheckListCollection(id);
			Comments = new CommentCollection(id);
			_description = new Field<string>(_context, () => Description);
			_dueDate = new Field<DateTime?>(_context, () => DueDate);
			_isArchived = new Field<bool?>(_context, () => IsArchived);
			_isArchived.AddRule(NullableHasValueRule<bool>.Instance);
			_isSubscribed = new Field<bool?>(_context, () => IsSubscribed);
			_isSubscribed.AddRule(NullableHasValueRule<bool>.Instance);
			Labels = new LabelCollection(_context);
			_lastActivity = new Field<DateTime?>(_context, () => LastActivity);
			_list = new Field<List>(_context, () => List);
			_list.AddRule(NotNullRule<List>.Instance);
			Members = new MemberCollection(id);
			_name = new Field<string>(_context, () => Name);
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, () => Position);
			_position.AddRule(NotNullRule<Position>.Instance);
			_position.AddRule(PositionRule.Instance);
			_shortId = new Field<int?>(_context, () => ShortId);
			_shortUrl = new Field<string>(_context, () => ShortUrl);
			_url = new Field<string>(_context, () => Url);

			if (cache)
				TrelloConfiguration.Cache.Add(this);
		}

		/// <summary>
		/// Applies the changes an action represents.
		/// </summary>
		/// <param name="action">The action.</param>
		void ICanWebhook.ApplyAction(Action action)
		{
			if (action.Type != ActionType.UpdateCard || action.Data.Card == null || action.Data.Card.Id != Id) return;
			_context.Merge(action.Data.Card.Json);
		}
		/// <summary>
		/// Deletes the card.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the card from Trello's server, however, this object will
		/// remain in memory and all properties will remain accessible.
		/// </remarks>
		public void Delete()
		{
			if (_deleted) return;

			_context.Delete();
			_deleted = true;
			TrelloConfiguration.Cache.Remove(this);
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			if (handler != null)
				handler(this, properties);
		}
	}
}