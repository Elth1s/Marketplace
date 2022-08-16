import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    IOrderStatus,
    IOrderStatusInfo,
    OrderStatusAction,
    OrderStatusActionTypes,
    ISearchOrderStatuses,
} from "./types"
import { ServerError } from "../../../store/types"

export const SearchOrderStatuses = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<OrderStatusAction>) => {
        try {
            let response = await http.get<ISearchOrderStatuses>(`api/OrderStatus/Search`, {
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
                type: OrderStatusActionTypes.SEARCH_ORDER_STATUSES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetOrderStatuses = () => {
    return async (dispatch: Dispatch<OrderStatusAction>) => {
        try {
            let response = await http.get<Array<IOrderStatusInfo>>(`api/OrderStatus/Get`)

            dispatch({
                type: OrderStatusActionTypes.GET_ORDER_STATUSES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetByIdOrderStatus = (id: number) => {
    return async (dispatch: Dispatch<OrderStatusAction>) => {
        try {
            let response = await http.get<IOrderStatus>(`api/OrderStatus/GetById/${id}`)

            dispatch({
                type: OrderStatusActionTypes.GET_BY_ID_ORDER_STATUS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateOrderStatus = (data: IOrderStatus) => {
    return async () => {
        try {
            await http.post("api/OrderStatus/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateOrderStatus = (id: number, data: IOrderStatus) => {
    return async () => {
        try {
            await http.put<IOrderStatus>(`api/OrderStatus/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteOrderStatus = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/OrderStatus/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteOrderStatuses = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/OrderStatus/Delete`, {
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