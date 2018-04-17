﻿namespace Manatee.Trello
{
	public interface IDropDownOption : ICacheable
	{
		ICustomField<IDropDownOption> Field { get; }
		string Text { get; }
		LabelColor? Color { get; }
		Position Position { get; }
	}
}