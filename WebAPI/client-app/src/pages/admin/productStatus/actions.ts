import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    IProductStatus,
    IProductStatusInfo,
    ProductStatusAction,
    ProductStatusActionTypes,
    ISearchProductStatuses,
} from "./types"
import { ServerError } from "../../../store/types"

export const SearchProductStatuses = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<ProductStatusAction>) => {
        try {
            let response = await http.get<ISearchProductStatuses>(`api/ProductStatus/Search`, {
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
                type: ProductStatusActionTypes.SEARCH_PRODUCT_STATUSES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetProductStatuses = () => {
    return async (dispatch: Dispatch<ProductStatusAction>) => {
        try {
            let response = await http.get<Array<IProductStatusInfo>>(`api/ProductStatus/Get`)

            dispatch({
                type: ProductStatusActionTypes.GET_PRODUCT_STATUSES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetByIdProductStatus = (id: number) => {
    return async (dispatch: Dispatch<ProductStatusAction>) => {
        try {
            let response = await http.get<IProductStatusInfo>(`api/ProductStatus/GetById/${id}`)

            dispatch({
                type: ProductStatusActionTypes.GET_BY_ID_PRODUCT_STATUS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateProductStatus = (data: IProductStatus) => {
    return async () => {
        try {
            await http.post("api/ProductStatus/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateProductStatus = (id: number, data: IProductStatus) => {
    return async () => {
        try {
            await http.put<IProductStatus>(`api/ProductStatus/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteProductStatus = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/ProductStatus/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteProductStatuses = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/ProductStatus/Delete`, {
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