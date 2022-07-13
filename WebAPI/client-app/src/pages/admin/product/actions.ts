import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    IProductInfo,
    ProductAction,
    ProductActionTypes,
    ISearchProducts,
} from "./types"
import { ServerError } from "../../../store/types"

export const SearchProducts = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.get<ISearchProducts>(`api/Product/Search`, {
                params: {
                    page: page,
                    rowsPerPage: rowsPerPage,
                    name: name,
                    isAscOrder: isAscOrder,
                    orderBy: orderBy
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            })

            dispatch({
                type: ProductActionTypes.SEARCH_PRODUCTS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetProducts = () => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.get<Array<IProductInfo>>(`api/Product/Get`)

            dispatch({
                type: ProductActionTypes.GET_PRODUCTS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteProducts = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/Product/Delete`, {
                params: {
                    ids: ids,
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            })
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}