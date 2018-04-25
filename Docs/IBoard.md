# IBoard

Represents a board.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IBoard

## Properties

### Manatee.Trello.IReadOnlyCollection`1[[Manatee.Trello.IAction, Manatee.Trello, Version=3.0.0.0, Culture=neutral, PublicKeyToken=f502fcc17fc907d6]] Actions { get; }

Gets the collection of actions performed on and within this board.

### Manatee.Trello.IReadOnlyCollection`1[[Manatee.Trello.ICard, Manatee.Trello, Version=3.0.0.0, Culture=neutral, PublicKeyToken=f502fcc17fc907d6]] Cards { get; }

Gets the collection of cards contained within this board.

#### Remarks

This property only exposes unarchived cards.

### DateTime CreationDate { get; }

Gets the creation date of the board.

### [ICustomFieldDefinitionCollection](ICustomFieldDefinitionCollection#icustomfielddefinitioncollection) CustomFields { get; }

Gets the collection of custom fields defined on the board.

### string Description { get; set; }

Gets or sets the board&#39;s description.

### bool? IsClosed { get; set; }

Gets or sets whether this board is closed.

### bool? IsPinned { get; set; }

Gets or sets wheterh this board is pinned.

### bool? IsStarred { get; set; }

Gets or sets wheterh this board is pinned.

### bool? IsSubscribed { get; set; }

Gets or sets whether the current member is subscribed to this board.

### [IList](IList#ilist) this[string key] { get; }

Retrieves a list which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching list, or null if none found.

#### Remarks

Matches on List.Id and List.Name. Comparison is case-sensitive.

### [IList](IList#ilist) this[int index] { get; }

Retrieves the list at the specified index.

**Parameter:** index

The index.

**Exception:** System.ArgumentOutOfRangeException

*index* is less than 0 or greater than or equal to the number of elements in the collection.

#### Returns

The list.

### [IBoardLabelCollection](IBoardLabelCollection#iboardlabelcollection) Labels { get; }

Gets the collection of labels for this board.

### DateTime? LastActivity { get; }

Gets the date of the board&#39;s most recent activity.

### DateTime? LastViewed { get; }

Gets the date when the board was most recently viewed.

### [IListCollection](IListCollection#ilistcollection) Lists { get; }

Gets the collection of lists on this board.

#### Remarks

This property only exposes unarchived lists.

### Manatee.Trello.IReadOnlyCollection`1[[Manatee.Trello.IMember, Manatee.Trello, Version=3.0.0.0, Culture=neutral, PublicKeyToken=f502fcc17fc907d6]] Members { get; }

Gets the collection of members on this board.

### [IBoardMembershipCollection](IBoardMembershipCollection#iboardmembershipcollection) Memberships { get; }

Gets the collection of members and their privileges on this board.

### string Name { get; set; }

Gets or sets the board&#39;s name.

### [IOrganization](IOrganization#iorganization) Organization { get; set; }

Gets or sets the organization to which this board belongs.

#### Remarks

Setting null makes the board&#39;s first admin the owner.

### [IBoardPersonalPreferences](IBoardPersonalPreferences#iboardpersonalpreferences) PersonalPreferences { get; }

Gets the set of preferences for the board.

### Manatee.Trello.IReadOnlyCollection`1[[Manatee.Trello.IPowerUpData, Manatee.Trello, Version=3.0.0.0, Culture=neutral, PublicKeyToken=f502fcc17fc907d6]] PowerUpData { get; }

Gets specific data regarding power-ups.

### Manatee.Trello.IReadOnlyCollection`1[[Manatee.Trello.IPowerUp, Manatee.Trello, Version=3.0.0.0, Culture=neutral, PublicKeyToken=f502fcc17fc907d6]] PowerUps { get; }

Gets metadata about any active power-ups.

### [IBoardPreferences](IBoardPreferences#iboardpreferences) Preferences { get; }

Gets the set of preferences for the board.

### string ShortLink { get; }

Gets the board&#39;s short URI.

### string ShortUrl { get; }

Gets the board&#39;s short link (ID).

### string Url { get; }

Gets the board&#39;s URI.

## Events

### Action&lt;IBoard, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the board is updated.

## Methods

### Task Delete(CancellationToken ct)

Deletes the board.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the board from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(CancellationToken ct)

Refreshes the board data.

**Parameter:** ct

(Optional) A cancellation token for async processing.
