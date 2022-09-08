import { Dispatch } from "react"

import http from "../../http_comon"
import { ProductAction, ProductActionTypes } from "../../pages/default/product/types";

import { ServerError } from "../../store/types"
import { BasketAction, BasketActionTypes, IBasketItem } from "./types";


export const BasketMenuChange = () => {
    return (dispatch: Dispatch<BasketAction>) => {
        dispatch({
            type: BasketActionTypes.BASKET_MENU_CHANGE
        })
    }
}

export const GetBasketItems = () => {
    return async (dispatch: Dispatch<BasketAction>) => {
        try {
            let response = await http.get<Array<IBasketItem>>(`api/BasketItem/GetAll`)

            dispatch({
                type: BasketActionTypes.GET_BASKET_ITEMS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateBasketItem = (basketId: number, count: number) => {
    return async () => {
        try {
            let response = await http.put(`api/BasketItem/UpdateBasket/${basketId}`, count)

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}


export const RemoveFromBasket = (id: number, isInBasket: boolean) => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.delete(`api/BasketItem/Delete/${id}`)

            if (isInBasket)
                dispatch({
                    type: ProductActionTypes.UPDATE_IS_IN_CART
                })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const ResetBasket = () => {
    return async (dispatch: Dispatch<BasketAction>) => {
        try {
            dispatch({
                type: BasketActionTypes.RESET_BASKET
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}