# 3.1.0

*Released on 1 Jun, 2018.*

<span id="trello">trello</span> <span id="feature">feature</span>

## Summary

### In line with changes in the Trello API:

([#178](https://github.com/gregsdennis/Manatee.Trello/issues/178)) Getting member avatar images have been augmented. Now the client must specify an image size.  The default is 170x170 which was previously the only option.  Now 30x30 and 50x50 are available as well. [Trello's change](https://trello.com/c/VX8B4ndj)

(no issue logged) Label uses have been removed from the API. [Trello's change](https://trello.com/c/qlIE6fkg)

(no issue logged) Custom fields can now be configured to show or not show on the front of a card. [Trello's change](https://trello.com/c/F3j0G136)

### New library features:

([#187](https://github.com/gregsdennis/Manatee.Trello/issues/187)) Starred boards are represented as objects in the Trello API.  Previous library versions only exposed `Board.IsStarred` as a read-only property.  These can now be listed and manipulated through the `StarredBoard` entity and its collection on the `Member` and `Me` entities, respectively.

([#224](https://github.com/gregsdennis/Manatee.Trello/issues/224)) Updated collection `Add()` methods to include optional parameters so that the data can be included as part of the creation process rather than having to set properties which would require at least one additional call.

([#211](https://github.com/gregsdennis/Manatee.Trello/issues/211)) All entities and collections can now be forcibly refreshed.

## Changes

New members:

- `static Meember.AvatarSize`
- `Member.Fields.AvatarUrl`
- `ICustomFieldDefinition.DisplayInfo`
- `CustomFieldDefinition.DisplayInfo`
- `IJsonMember.StarredBoards`
- `Member.StarredBoards`
- `Me.StarredBoards`
- Optional `description` parameter for `IBoardCollection.Add()`
- Optional `description` parameter for `BoardCollection.Add()`
- Optional `position` parameter for `IListCollection.Add()`
- Optional `position` parameter for `ListCollection.Add()`
- Optional `description` and `name` parameters for `IOrganizationCollection.Add()`
- Optional `description` and `name` parameters for `OrganizationCollection.Add()`

New types:

- `AvatarSize`
- `ICustomFieldDisplayInfo`
- `CustomFieldDisplayInfo`
- `IJsonStarredBoard`
- `StarredBoard`
- `ReadOnlyStarredBoardCollection`
- `IStarredBoardCollection`
- `StarredBoardCollection`

Functional changes:

- `Member.AvatarUrl` now returns sized image assigned by `static Member.AvatarSize`

Obsoleted the following:

- `Member.Fields.AvatarHash`
- `Member.Fields.AvatarSource`
- `Member.Fields.GravatarHash`
- `Member.Fields.UploadedAvatarHash`
- `Member.AvatarSource` (now just returns null)
- `Label.Uses` (now just returns null)
- `IJsonLabel.Uses`

# 3.0.12

*Released on 30 May, 2018.*

<span id="feature">feature</span> <span id="patch">patch</span> 

Fixes issue for webhook processing where the property list provided by the event were inaccurate.  Also resolves an issue of updating cached entities with potentially stale data downloaded from `Action`s that indicated past activities.  **As a result, `Action.Data` and `Notification.Data` no longer use cached entities.**

Additionally, the properties reported for sub-entities (e.g. `Card.Badges`) are now prefixed with the container property.  So if `Card.Badges.Comments` (a count of the comments on the card) changes, the `Card.Updated` event would report that `Badges.Comments` was updated.  Previously, the property report would be only `Comments` which conflicts with the `Card.Comments` property.

Fixed a deserialization issue for cards.  `ShortLink` does not directly translate to `ShortUrl` and needs some formatting.

`Refresh()` on collection types is no longer virtual.  (Should have been sealed on all implementations anyway.)

# 3.0.11

*Released on 18 May, 2018.*

<span id="patch">patch</span> 

Fixed issue for all entities where processing webhook data would not fire the `Updated` event.

# 3.0.10

*Released on 14 May, 2018.*

<span id="patch">patch</span> 

Attachment image previews have their ID property serialized as `_id` rather than `id`.

# 3.0.9

*Released on 12 May, 2018.*

<span id="patch">patch</span> 

Changed serialization of numbers when setting custom field values to use invariant culture.

# 3.0.8

*Released on 12 May, 2018.*

<span id="feature">feature</span> <span id="patch">patch</span> 

Updated file location for license usage details to local app data for the current user.

Updated power-up implementation:

- `IBoard.PowerUps` is now `IPowerUpCollection` (was `IReadOnlyCollection<IPowerUp>`)
    - Adds `EnablePowerUp()` and `DisablePowerUp()`
- Fixed issues with setting number, string, and drop-down custom fields

# 3.0.7

*Released on 11 May, 2018.*

<span id="patch">patch</span> 

Bug fix for setting dropdown and text custom fields on cards without values.

Internal updates to collection classes.

# 3.0.6

*Released on 7 May, 2018.*

<span id="patch">patch</span> 

Updated boards and cards to only cache themselves once the full ID has been downloaded.

# 3.0.5

*Released on 4 May, 2018.*

<span id="patch">patch</span> 

Fixed further issues with deserialization.

- `IJsonBoard`
- `IJsonList`
- `IJsonOrganizationMembership`
- `IJsonToken`

# 3.0.4

*Released on 4 May, 2018.*

<span id="patch">patch</span> 

Fixed issue with `IJsonBoardBackground` deserialization.

# 3.0.3

*Released on 3 May, 2018.*

<span id="patch">patch</span> 

Fixed issue with `DropDownOption` that caused `ArgumentNullException` when attempting to add the entity to the cache.

# 3.0.2

*Released on 2 May, 2018.*

<span id="trello">trello</span> <span id="feature">feature</span> <span id="patch">patch</span> 

The following are now read-only as these requests are not supported by Trello.

- `Board.IsPinned`
- `Board.IsStarred`
- `CheckList.Card`

`ICache` changed to take `ICacheable` instead of any object to help support better thread safety.

`Webhook` now implements `ICacheable`

# 3.0.1

*Released on 1 May, 2018.*

<span id="patch">patch</span> 

Fixed description attribute for:

- `CheckList.Fields.Board`
- `CheckList.Fields.Card`

`IDropDownCollection` fixed to inherit `IReadOnlyCollection<IDropDownOption>`.

Added missing `DropDownOption` method to `TrelloFactory` to provide a mechanism for creating new options for custom fields.  Also added matching `static DropDownOption.Create()` method.

Added `CheckList` to `IJsonCheckItem`

Fixed serialization issues for:

- cards
- check items
- check lists

# 3.0.0

*Released on 27 Apr, 2018.*

<span id="break">breaking change</span> <span id="trello">trello</span> <span id="feature">feature</span> <span id="patch">patch</span> 

## Supported frameworks

Now multi-targets:

- .Net Framework 4.5
- .Net Standard 1.3
- .Net Standard 2.0

## Added asynchronous processing

All methods that perform requests (`Refresh()`, `Delete()`, collection `Add()` methods, etc.) are now async methods and should be awaited.

Request processing no longer occurs in a queue.  Instead, the .Net *async/await* model is used.

## Data access

Getting properties no longer produces requests.  Calling `Refresh()` is required.

Setting properties works as before.

When refreshing entities and collections, more data is downloaded with a single call.  Additionally, all data is used to update any available entities.  This results in fewer required calls.

## Entities

Interfaces have been extracted for all entities to support unit testing.

All properties have been altered to return interfaces rather than concrete types.

## Configuration

Added `RefreshThrottle` to limit successive GET requests.
Added `RemoveDeletedItemsFromCache` to optionally keep deleted entities in the cache.
Removed `ExpiryTime` in conjuction with changes to property getters.

## Libraries

The following libraries have been consolidated into the main library:

- *Manatee.Trello.ManateeJson*
- *Manatee.Trello.WebApi*
- *Manatee.Trello.CustomFields*

The configuration seams for these libraries are still available if alternate solutions are desired.

### Additional changes

Custom fields are now writable.