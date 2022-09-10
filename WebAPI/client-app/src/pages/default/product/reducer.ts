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
                reviews: [...state.reviews, ...action.payload.values]
            }
        case ReviewActionTypes.GET_REPLIES_FOR_REVIEW:
            let reviewReply = state.reviews.find(value => value.id === action.payload.id);
            if (reviewReply) {
                let index = state.reviews.indexOf(reviewReply);
                let tempReviews = state.reviews.slice();

                tempReviews[index].replies = action.payload.values;
                tempReviews[index].repliesCount = action.payload.count;
                return {
                    ...state,
                    reviews: tempReviews
                }
            }
            else
                return state;
        case ReviewActionTypes.GET_MORE_REPLIES_FOR_REVIEW:
            let reviewReplyMore = state.reviews.find(value => value.id === action.payload.id);
            if (reviewReplyMore) {
                let index = state.reviews.indexOf(reviewReplyMore);
                let tempReviews = state.reviews.slice();

                tempReviews[index].replies = [...tempReviews[index].replies, ...action.payload.values];
                tempReviews[index].repliesCount = action.payload.count;
                return {
                    ...state,
                    reviews: tempReviews
                }
            }
            else
                return state;
        case ReviewActionTypes.CHANGE_REVIEW_LIKE:
            let reviewLike = state.reviews.find(value => value.id === action.payload);
            if (reviewLike) {
                let index = state.reviews.indexOf(reviewLike);
                let tempReviews = state.reviews.slice();

                if (tempReviews[index].isDisliked) {
                    tempReviews[index].isDisliked = !tempReviews[index].isDisliked;
                    tempReviews[index].dislikes = tempReviews[index].dislikes - 1;
                }

                if (tempReviews[index].isLiked)
                    tempReviews[index].likes = tempReviews[index].likes - 1;
                else
                    tempReviews[index].likes = tempReviews[index].likes + 1;

                tempReviews[index].isLiked = !tempReviews[index].isLiked;
                return {
                    ...state,
                    reviews: tempReviews
                }
            }
            else
                return state;
        case ReviewActionTypes.CHANGE_REVIEW_DISLIKE:
            let reviewDislike = state.reviews.find(value => value.id === action.payload);
            if (reviewDislike) {
                let index = state.reviews.indexOf(reviewDislike);
                let tempReviews = state.reviews.slice();

                if (tempReviews[index].isLiked) {
                    tempReviews[index].isLiked = !tempReviews[index].isLiked;
                    tempReviews[index].likes = tempReviews[index].likes - 1;
                }

                if (tempReviews[index].isDisliked)
                    tempReviews[index].dislikes = tempReviews[index].dislikes - 1;
                else
                    tempReviews[index].dislikes = tempReviews[index].dislikes + 1;

                tempReviews[index].isDisliked = !tempReviews[index].isDisliked;
                return {
                    ...state,
                    reviews: tempReviews
                }
            }
            else
                return state;
        case QuestionActionTypes.GET_QUESTIONS:
            return {
                ...state,
                questions: action.payload.values,
                questionsCount: action.payload.count
            }
        case QuestionActionTypes.GET_MORE_QUESTIONS:
            return {
                ...state,
                questions: [...state.questions, ...action.payload.values]
            }
        case QuestionActionTypes.GET_REPLIES_FOR_QUESTION:
            let questionReply = state.questions.find(value => value.id === action.payload.id);
            if (questionReply) {
                let index = state.questions.indexOf(questionReply);
                let tempQuestions = state.questions.slice();

                tempQuestions[index].replies = action.payload.values;
                tempQuestions[index].repliesCount = action.payload.count;
                return {
                    ...state,
                    questions: tempQuestions
                }
            }
            else
                return state;
        case QuestionActionTypes.GET_MORE_REPLIES_FOR_QUESTION:
            let questionReplyMore = state.questions.find(value => value.id === action.payload.id);
            if (questionReplyMore) {
                let index = state.questions.indexOf(questionReplyMore);
                let tempQuestions = state.questions.slice();

                tempQuestions[index].replies = [...tempQuestions[index].replies, ...action.payload.values];
                tempQuestions[index].repliesCount = action.payload.count;
                return {
                    ...state,
                    questions: tempQuestions
                }
            }
            else
                return state;
        case QuestionActionTypes.CHANGE_QUESTION_LIKE:
            let questionLike = state.questions.find(value => value.id === action.payload);
            if (questionLike) {
                let index = state.questions.indexOf(questionLike);
                let tempQuestions = state.questions.slice();

                if (tempQuestions[index].isDisliked) {
                    tempQuestions[index].isDisliked = !tempQuestions[index].isDisliked;
                    tempQuestions[index].dislikes = tempQuestions[index].dislikes - 1;
                }

                if (tempQuestions[index].isLiked)
                    tempQuestions[index].likes = tempQuestions[index].likes - 1;
                else
                    tempQuestions[index].likes = tempQuestions[index].likes + 1;

                tempQuestions[index].isLiked = !tempQuestions[index].isLiked;
                return {
                    ...state,
                    questions: tempQuestions
                }
            }
            else
                return state;
        case QuestionActionTypes.CHANGE_QUESTION_DISLIKE:
            let questionDislike = state.questions.find(value => value.id === action.payload);
            if (questionDislike) {
                let index = state.questions.indexOf(questionDislike);
                let tempQuestions = state.questions.slice();

                if (tempQuestions[index].isLiked) {
                    tempQuestions[index].isLiked = !tempQuestions[index].isLiked;
                    tempQuestions[index].likes = tempQuestions[index].likes - 1;
                }

                if (tempQuestions[index].isDisliked)
                    tempQuestions[index].dislikes = tempQuestions[index].dislikes - 1;
                else
                    tempQuestions[index].dislikes = tempQuestions[index].dislikes + 1;

                tempQuestions[index].isDisliked = !tempQuestions[index].isDisliked;
                return {
                    ...state,
                    questions: tempQuestions
                }
            }
            else
                return state;
        default:
            return state;
    }
}