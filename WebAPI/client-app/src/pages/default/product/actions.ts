import qs from "qs"
import { Dispatch } from "react"

import http from "../../../http_comon"
import { ServerError } from "../../../store/types"
import { IProductWithParents, IQuestion, IQuestionItem, IQuestionWithCount, IReview, IReviewItem, IReviewWithCount, ISimilarProduct, ProductAction, ProductActionTypes, QuestionAction, QuestionActionTypes, ReviewAction, ReviewActionTypes } from "./types"

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

export const GetSimilarProducts = (urlSlug: string) => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.get<Array<ISimilarProduct>>(`api/Product/GetSimilarProducts`, {
                params: {
                    urlSlug: urlSlug,
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: ProductActionTypes.GET_SIMILAR_PRODUCTS,
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
                type: ProductActionTypes.UPDATE_SELECTED_PRODUCT,
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
            let response = await http.get<Array<IReviewItem>>(`api/Review/GetForProduct`, {
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
            let response = await http.get<Array<IQuestionItem>>(`api/Question/GetForProduct`, {
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