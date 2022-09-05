import { ProductAction, ProductActionTypes, ProductState, QuestionAction, QuestionActionTypes, ReviewAction, ReviewActionTypes } from "./types";

const initialState: ProductState = {
    parents: [],
    product: {
        isInBasket: false,
        isSelected:false,
        name: "",
        shopId: 0,
        shopName: "",
        productStatus: "",
        shopRating: 0,
        productRating: 0,
        countReviews: 0,
        images: [],
        price: 0,
        filters: []
    },
    similarProducts: [],
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
        case ProductActionTypes.GET_SIMILAR_PRODUCTS:
            return {
                ...state,
                similarProducts: action.payload,
            }
        case ProductActionTypes.UPDATE_SELECTED_PRODUCT:
            return {
                ...state,
                product: {
                    ...state.product,
                    isInBasket: !state.product.isInBasket
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