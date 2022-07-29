import qs from "qs"
import { Dispatch } from "react"

import http from "../../../http_comon"
import { ServerError } from "../../../store/types"
import { IProductWithParents, ISimilarProduct, ProductAction, ProductActionTypes } from "./types"

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