export interface PlayerJournalModel {
    journalItemId : string,
    journalId : string,
    playerId : string,
    boughtPrice : string,
    quantity  : string,
    description  : string,
    soldPrice  : string,
    createdDate  : string,
    soldDate  : string,
    deletedDate  : string,

    notificationId : string,
    alertPrice : string,
    isExpired : string,
    
    playerFullName : string,
    playerCardTypeId : string,
    facePhotoUrl : string,
    badgePhotoUrl : string,
    nationPhotoUrl : string,
    overall : string,
    currentPricePS4 : string,
    position : string,
    lastUpdatedDate : string,

    totalInvestment : string,
    profitLossPerCard : string,
    totalInvestmentPL : string,
    overallColor: string
}