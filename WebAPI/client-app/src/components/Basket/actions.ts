import { Dispatch } from "react"

import http from "../../http_comon"

import { ServerError } from "../../store/types"
import { BasketAction, BasketActionTypes, IBasketItem } from "./types";



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