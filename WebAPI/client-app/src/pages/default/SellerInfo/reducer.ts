import { ShopReviewAction, ShopPageState, ShopReviewActionTypes, ShopPageAction, ShopPageActionTypes } from "./types";

const initialState: ShopPageState = {
    shopReviews: [],
    shopReviewsCount: 0,
    shopPageInfo: {
        name: "",
        description: "",
        averageServiceQualityRating: 0,
        averageTimelinessRating: 0,
        averageInformationRelevanceRating: 0,
        countReviews: 0,
        averageRating: 0,
        ratings: [],
        schedule: []
    }
}

export const shopPageReducer = (state = initialState, action: ShopReviewAction | ShopPageAction): ShopPageState => {
    switch (action.type) {
        case ShopReviewActionTypes.GET_SHOP_REVIEWS:
            return {
                ...state,
                shopReviews: action.payload.values,
                shopReviewsCount: action.payload.count
            }
        case ShopReviewActionTypes.GET_MORE_SHOP_REVIEWS:
            return {
                ...state,
                shopReviews: [...state.shopReviews, ...action.payload]
            }
        case ShopPageActionTypes.GET_SHOP_PAGE_INFO:
            return {
                ...state,
                shopPageInfo: action.payload
            }
        default:
            return state;
    }
}