import qs from "qs"
import { Dispatch } from "react"

import http from "../../../http_comon"
import { ServerError } from "../../../store/types"
import { IProductRating, IProductWithParents, IQuestion, IQuestionItem, IQuestionWithCount, IReply, IReplyWithCount, IReview, IReviewItem, IReviewWithCount, ISimilarProduct, ProductAction, ProductActionTypes, QuestionAction, QuestionActionTypes, ReviewAction, ReviewActionTypes } from "./types"

export const GetProductByUrlSlug = (urlSlug: string) => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.get<IProductWithParents>(`api/Product/GetByUrlSlug`, {
                params: {
                    urlSlug: urlSlug,
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: ProductActionTypes.GET_PRODUCT_BY_URLSLUG,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetProductRatingByUrlSlug = (urlSlug: string) => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.get<IProductRating>(`api/Product/GetProductRatingByUrlSlug`, {
                params: {
                    urlSlug: urlSlug,
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: ProductActionTypes.GET_PRODUCT_RATING_BY_URL_SLUG,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const AddProductInCart = (urlSlug: string) => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.post(`api/BasketItem/Create`, { urlSlug: urlSlug });

            dispatch({
                type: ProductActionTypes.UPDATE_IS_IN_CART,
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const AddProductInSelected = (urlSlug: string) => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.put(`api/Product/ChangeSelectProduct/${urlSlug}`);

            dispatch({
                type: ProductActionTypes.UPDATE_IS_SELECTED,
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetReviews = (productSlug: string, page: number, rowsPerPage: number) => {
    return async (dispatch: Dispatch<ReviewAction>) => {
        try {
            let response = await http.get<IReviewWithCount>(`api/Review/GetForProduct`, {
                params: {
                    productSlug: productSlug,
                    page: page,
                    rowsPerPage: rowsPerPage
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: ReviewActionTypes.GET_REVIEWS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetMoreReviews = (productSlug: string, page: number, rowsPerPage: number) => {
    return async (dispatch: Dispatch<ReviewAction>) => {
        try {
            let response = await http.get<IReviewWithCount>(`api/Review/GetForProduct`, {
                params: {
                    productSlug: productSlug,
                    page: page,
                    rowsPerPage: rowsPerPage
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: ReviewActionTypes.GET_MORE_REVIEWS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetRepliesForReview = (reviewId: number, page: number, rowsPerPage: number) => {
    return async (dispatch: Dispatch<ReviewAction>) => {
        try {
            let response = await http.get<IReplyWithCount>(`api/ReviewReply/GetByReview`, {
                params: {
                    reviewId: reviewId,
                    page: page,
                    rowsPerPage: rowsPerPage
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            response.data.id = reviewId;

            dispatch({
                type: ReviewActionTypes.GET_REPLIES_FOR_REVIEW,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetMoreRepliesForReview = (reviewId: number, page: number, rowsPerPage: number) => {
    return async (dispatch: Dispatch<ReviewAction>) => {
        try {
            let response = await http.get<IReplyWithCount>(`api/ReviewReply/GetByReview`, {
                params: {
                    reviewId: reviewId,
                    page: page,
                    rowsPerPage: rowsPerPage
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            response.data.id = reviewId;

            dispatch({
                type: ReviewActionTypes.GET_MORE_REPLIES_FOR_REVIEW,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const AddReview = (values: IReview) => {
    return async () => {
        try {
            let response = await http.post(`api/Review/Create`, values);

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetQuestions = (productSlug: string, page: number, rowsPerPage: number) => {
    return async (dispatch: Dispatch<QuestionAction>) => {
        try {
            let response = await http.get<IQuestionWithCount>(`api/Question/GetForProduct`, {
                params: {
                    productSlug: productSlug,
                    page: page,
                    rowsPerPage: rowsPerPage
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: QuestionActionTypes.GET_QUESTIONS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetMoreQuestions = (productSlug: string, page: number, rowsPerPage: number) => {
    return async (dispatch: Dispatch<QuestionAction>) => {
        try {
            let response = await http.get<IQuestionWithCount>(`api/Question/GetForProduct`, {
                params: {
                    productSlug: productSlug,
                    page: page,
                    rowsPerPage: rowsPerPage
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: QuestionActionTypes.GET_MORE_QUESTIONS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetRepliesForQuestion = (questionId: number, page: number, rowsPerPage: number) => {
    return async (dispatch: Dispatch<QuestionAction>) => {
        try {
            let response = await http.get<IReplyWithCount>(`api/QuestionReply/GetByQuestion`, {
                params: {
                    questionId: questionId,
                    page: page,
                    rowsPerPage: rowsPerPage
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            response.data.id = questionId;

            dispatch({
                type: QuestionActionTypes.GET_REPLIES_FOR_QUESTION,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetMoreRepliesForQuestion = (questionId: number, page: number, rowsPerPage: number) => {
    return async (dispatch: Dispatch<QuestionAction>) => {
        try {
            let response = await http.get<IReplyWithCount>(`api/QuestionReply/GetByQuestion`, {
                params: {
                    questionId: questionId,
                    page: page,
                    rowsPerPage: rowsPerPage
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            response.data.id = questionId;

            dispatch({
                type: QuestionActionTypes.GET_MORE_REPLIES_FOR_QUESTION,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const AddQuestion = (values: IQuestion) => {
    return async () => {
        try {
            let response = await http.post(`api/Question/Create`, values);

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const ReviewLike = (id: number) => {
    return async (dispatch: Dispatch<ReviewAction>) => {
        try {
            await http.post(`api/Review/Like/${id}`);

            dispatch({
                type: ReviewActionTypes.CHANGE_REVIEW_LIKE,
                payload: id
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const ReviewDislike = (id: number) => {
    return async (dispatch: Dispatch<ReviewAction>) => {
        try {
            await http.post(`api/Review/Dislike/${id}`);

            dispatch({
                type: ReviewActionTypes.CHANGE_REVIEW_DISLIKE,
                payload: id
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const AddReviewReply = (value: IReply, id: number) => {
    return async () => {
        try {
            await http.post(`api/ReviewReply/Create/`, { fullName: value.fullName, email: value.email, text: value.text, reviewId: id });

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const QuestionLike = (id: number) => {
    return async (dispatch: Dispatch<QuestionAction>) => {
        try {
            await http.post(`api/Question/Like/${id}`);

            dispatch({
                type: QuestionActionTypes.CHANGE_QUESTION_LIKE,
                payload: id
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const QuestionDislike = (id: number) => {
    return async (dispatch: Dispatch<QuestionAction>) => {
        try {
            await http.post(`api/Question/Dislike/${id}`);

            dispatch({
                type: QuestionActionTypes.CHANGE_QUESTION_DISLIKE,
                payload: id
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const AddQuestionReply = (value: IReply, id: number) => {
    return async () => {
        try {
            await http.post(`api/QuestionReply/Create/`, { fullName: value.fullName, email: value.email, text: value.text, questionId: id });

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}