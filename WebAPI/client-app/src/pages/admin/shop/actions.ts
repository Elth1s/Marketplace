import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    IShop,
    IShopInfo,
    ShopAction,
    ShopActionTypes,
    ISearchShops,
} from "./types"
import { ServerError } from "../../../store/types"

export const SearchShops = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<ShopAction>) => {
        try {
            let response = await http.get<ISearchShops>(`api/Shop/Search`, {
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
                type: ShopActionTypes.SEARCH_SHOPS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetShops = () => {
    return async (dispatch: Dispatch<ShopAction>) => {
        try {
            let response = await http.get<Array<IShopInfo>>(`api/Shop/Get`)

            dispatch({
                type: ShopActionTypes.GET_SHOPS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetByIdShop = (id: number) => {
    return async (dispatch: Dispatch<ShopAction>) => {
        try {
            let response = await http.get<IShop>(`api/Shop/GetById/${id}`)

            dispatch({
                type: ShopActionTypes.GET_BY_ID_SHOP,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}


export const DeleteShops = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/Shop/Delete`, {
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