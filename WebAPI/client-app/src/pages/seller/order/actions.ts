import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"

import { ServerError } from "../../../store/types"

import { IOrderFullInfo, IOrderUpdate, ISearchOrders, OrderAction, OrderActionTypes } from "./type"

export const AdminSellerSearchOrders = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<OrderAction>) => {
        try {
            let response = await http.get<ISearchOrders>(`api/Order/AdminSellerSearch`, {
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
                type: OrderActionTypes.SEARCH_ORDERS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetOrderByIdForSeller = (id: number) => {
    return async (dispatch: Dispatch<OrderAction>) => {
        try {
            let response = await http.get<IOrderFullInfo>(`api/Order/GetByIdOrder/${id}`)

            dispatch({
                type: OrderActionTypes.GET_BY_ID,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateOrderForSeller = (id: number, values: IOrderUpdate) => {
    return async () => {
        try {
            let response = await http.put(`api/Order/UpdateOrder/${id}`, values)

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}