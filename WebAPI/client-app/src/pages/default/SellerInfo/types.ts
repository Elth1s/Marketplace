export enum ShopReviewActionTypes {
    GET_SHOP_REVIEWS = "GET_SHOP_REVIEWS",
    GET_MORE_SHOP_REVIEWS = "GET_MORE_SHOP_REVIEWS",
}
export enum ShopPageActionTypes {
    GET_SHOP_PAGE_INFO = "GET_SHOP_PAGE_INFO",
}

export interface IShopReviewItem {
    id: number,
    fullName: string,
    serviceQualityRating: number,
    timelinessRating: number,
    informationRelevanceRating: number,
    date: string,
    comment: string,
}
export interface IShopReview {
    fullName: string,
    email: string,
    serviceQualityRating: number,
    timelinessRating: number,
    informationRelevanceRating: number,
    comment: string,
    shopId: number
}
export interface IShopPageInfo {
    name: string,
    description: string,
    averageServiceQualityRating: number,
    averageTimelinessRating: number,
    averageInformationRelevanceRating: number,
    countReviews: number,
    averageRating: number,
    ratings: Array<IRating>
    schedule: Array<IScheduleItem>
}

export interface IRating {
    number: number,
    count: number
}
export interface IScheduleItem {
    start: string,
    end: string,
    isWeekend: boolean,
    shortNames: string
}

export interface ShopPageState {
    shopReviews: Array<IShopReviewItem>,
    shopReviewsCount: number,
    shopPageInfo: IShopPageInfo
}

export interface IShopReviewWithCount {
    values: Array<IShopReviewItem>,
    count: number,
}

export interface GetShopReviewsAction {
    type: ShopReviewActionTypes.GET_SHOP_REVIEWS,
    payload: IShopReviewWithCount
}

export interface GetMoreShopReviewsAction {
    type: ShopReviewActionTypes.GET_MORE_SHOP_REVIEWS,
    payload: Array<IShopReviewItem>
}

export interface GetShopPageInfoAction {
    type: ShopPageActionTypes.GET_SHOP_PAGE_INFO,
    payload: IShopPageInfo
}

export type ShopReviewAction = GetShopReviewsAction |
    GetMoreShopReviewsAction;

export type ShopPageAction = GetShopPageInfoAction;

