# API Documentation

This section provides detailed information about the API for the project.

## Framework

- [GameAddressableContext](xref:Framework.GameAddressableContext)
- [GameController](xref:Framework.GameController-1)
- [GameDependency](xref:Framework.GameDependency)
- [GameEventBusImpl](xref:Framework.GameEventBusImpl)
- [GameEventBusImpl.DelegateTask](xref:Framework.GameEventBusImpl.DelegateTask)
- [GameFieldAttributes](xref:Framework.GameFieldAttributes)
- [GameFieldAttributes.ControllerFieldAttribute](xref:Framework.GameFieldAttributes.ControllerFieldAttribute)
- [GameFieldAttributes.ModelFieldAttribute](xref:Framework.GameFieldAttributes.ModelFieldAttribute)
- [GameMethod](xref:Framework.GameMethod)
- [GameMethodEvents](xref:Framework.GameMethodEvents)
- [GameMethods](xref:Framework.GameMethods)
- [GameModel](xref:Framework.GameModel)
- [GameView](xref:Framework.GameView)
- [GameViewUI](xref:Framework.GameViewUI)
- [IGameEventBus](xref:Framework.IGameEventBus)

## Game Entities

### Card

- [CardAnimationController](xref:Game.Entities.Card.CardAnimationController)
- [CardEventController](xref:Game.Entities.Card.CardEventController)
- [CardImageController](xref:Game.Entities.Card.CardImageController)
- [CardModel](xref:Game.Entities.Card.CardModel)
- [CardView](xref:Game.Entities.Card.CardView)
- [CardShape](xref:Game.Entities.Card.Data.CardShape)
- [CardShapeExtension](xref:Game.Entities.Card.Data.CardShapeExtension)
- [CardType](xref:Game.Entities.Card.Data.CardType)
- [CardTypeExtension](xref:Game.Entities.Card.Data.CardTypeExtension)
- [OnCardFlippedEvent](xref:Game.Entities.Card.Data.OnCardFlippedEvent)
- [OnCardSetupEvent](xref:Game.Entities.Card.Data.OnCardSetupEvent)

### CardGrid

- [CardGridController](xref:Game.Entities.CardGrid.CardGridController)
- [CardGridModel](xref:Game.Entities.CardGrid.CardGridModel)
- [CardGridView](xref:Game.Entities.CardGrid.CardGridView)
- [OnCardGridSetupEvent](xref:Game.Entities.CardGrid.Data.OnCardGridSetupEvent)

### UIManager

- [UIManagerController](xref:Game.Entities.UIManager.UIManagerController)
- [UIManagerModel](xref:Game.Entities.UIManager.UIManagerModel)
- [UIManagerView](xref:Game.Entities.UIManager.UIManagerView)

### UIMenu

- [UIMenuController](xref:Game.Entities.UIMenu.UIMenuController)
- [UIMenuModel](xref:Game.Entities.UIMenu.UIMenuModel)
- [UIMenuView](xref:Game.Entities.UIMenu.UIMenuView)
- [OnUIMenuEnableEvent](xref:Game.Entities.UIMenu.Data.OnUIMenuEnableEvent)

### UITextElement

- [UITextElementController](xref:Game.Entities.UITextElement.UITextElementController)
- [UITextElementModel](xref:Game.Entities.UITextElement.UITextElementModel)
- [UITextElementView](xref:Game.Entities.UITextElement.UITextElementView)
- [OnUIManagerUpdateEvent](xref:Game.Entities.UITextElement.Data.OnUIManagerUpdateEvent)

## Game Helper

- [ListExtensions](xref:Game.Helper.ListExtensions)
- [ResponsiveGridLayout](xref:Game.Helper.ResponsiveGridLayout)
- [TransformExtension](xref:Game.Helper.TransformExtension)

## Game Services

### Data

- [AudioData.Music](xref:Game.Services.Data.AudioData.Music)
- [AudioData.Sfx](xref:Game.Services.Data.AudioData.Sfx)
- [AudioData.Voice](xref:Game.Services.Data.AudioData.Voice)
- [OnRoundCardMatchEvent](xref:Game.Services.Data.OnRoundCardMatchEvent)
- [OnRoundCardMissMatchEvent](xref:Game.Services.Data.OnRoundCardMissMatchEvent)
- [OnRoundEndEvent](xref:Game.Services.Data.OnRoundEndEvent)
- [OnRoundStartEvent](xref:Game.Services.Data.OnRoundStartEvent)
- [ScoringData](xref:Game.Services.Data.ScoringData)

### Interfaces

- [IAudioService](xref:Game.Services.IAudioService)
- [IGameAddressablesService](xref:Game.Services.IGameAddressablesService)
- [IGameService](xref:Game.Services.IGameService)
- [ILevelService](xref:Game.Services.ILevelService)
- [ILogService](xref:Game.Services.ILogService)
- [IRoundService](xref:Game.Services.IRoundService)
- [IScoringService](xref:Game.Services.IScoringService)
- [IStorageService](xref:Game.Services.IStorageService)

### Implementations

- [AudioServiceImpl](xref:Game.Services.Impl.AudioServiceImpl)
- [GameAddressablesServiceImpl](xref:Game.Services.Impl.GameAddressablesServiceImpl)
- [GameServiceImpl](xref:Game.Services.Impl.GameServiceImpl)
- [LevelServiceImpl](xref:Game.Services.Impl.LevelServiceImpl)
- [LogServiceImpl](xref:Game.Services.Impl.LogServiceImpl)
- [RoundServiceImpl](xref:Game.Services.Impl.RoundServiceImpl)
- [ScoringServiceImpl](xref:Game.Services.Impl.ScoringServiceImpl)
- [StorageServiceImpl](xref:Game.Services.Impl.StorageServiceImpl)

### Lifetime

- [IStartableService](xref:Game.Services.Lifetime.IStartableService)
- [IStoppableService](xref:Game.Services.Lifetime.IStoppableService)
- [ITickableService](xref:Game.Services.Lifetime.ITickableService)