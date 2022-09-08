import { ProductAction, ProductActionTypes, ProductState, QuestionAction, QuestionActionTypes, ReviewAction, ReviewActionTypes } from "./types";

const initialState: ProductState = {
    parents: [],
    productRating: {
        rating: 0,
        countReviews: 0,
    },
    product: {
        isInBasket: false,
        isSelected: false,
        name: "",
        shopId: 0,
        shopName: "",
        productStatus: "",
        shopRating: 0,
        images: [],
        price: 0,
        filters: []
    },
    reviews: [],
    reviewsCount: 0,
    questions: [],
    questionsCount: 0
}

export const productReducer = (state = initialState, action: ProductAction | ReviewAction | QuestionAction): ProductState => {
    switch (action.type) {
        case ProductActionTypes.GET_PRODUCT_BY_URLSLUG:
            return {
                ...state,
                parents: action.payload.parents,
                product: action.payload.product,
            }
        case ProductActionTypes.GET_PRODUCT_RATING_BY_URL_SLUG:
            return {
                ...state,
                productRating: action.payload,
            }
        case ProductActionTypes.UPDATE_IS_IN_CART:
            return {
                ...state,
                product: {
                    ...state.product,
                    isInBasket: !state.product.isInBasket
                }
            }
        case ProductActionTypes.UPDATE_IS_SELECTED:
            return {
                ...state,
                product: {
                    ...state.product,
                    isSelected: !state.product.isSelected
                }
            }
        case ReviewActionTypes.GET_REVIEWS:
            return {
                ...state,
                reviews: action.payload.values,
                reviewsCount: action.payload.count
            }
        case ReviewActionTypes.GET_MORE_REVIEWS:
            return {
                ...state,
                reviews: [...state.reviews, ...action.payload]
            }
        case QuestionActionTypes.GET_QUESTIONS:
            return {
                ...state,
                questions: action.payload.values,
                questionsCount: action.payload.count
            }
        case QuestionActionTypes.GET_MORE_QUESTIONS:
            return {
                ...state,
                questions: [...state.questions, ...action.payload]
            }
        default:
            return state;
    }
}