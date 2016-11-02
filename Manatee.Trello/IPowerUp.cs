﻿using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public interface IPowerUp : ICacheable
	{
		string Name { get; }
		bool? Public { get; }
		string AdditionalInfo { get; }
	}

	public abstract class PowerUpBase : IPowerUp
	{
		private readonly Field<string> _name;
		private readonly Field<bool?> _public;
		private readonly Field<string> _additionalInfo;
		private readonly PowerUpContext _context;

		public string AdditionalInfo => _additionalInfo.Value;
		/// <summary>
		/// Gets the board's ID.
		/// </summary>
		public string Id { get; }
		public string Name => _name.Value;
		/// <summary>
		/// Gets or sets whether this board is closed.
		/// </summary>
		public bool? Public => _public.Value;

		internal IJsonPowerUp Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		protected PowerUpBase(IJsonPowerUp json, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new PowerUpContext(Id, auth);

			_additionalInfo = new Field<string>(_context, nameof(AdditionalInfo));
			_name = new Field<string>(_context, nameof(Name));
			_public = new Field<bool?>(_context, nameof(Public));
		}
	}

	public class UnknownPowerUp : PowerUpBase
	{
		internal UnknownPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
			: base(json, auth) {}
	}
}
