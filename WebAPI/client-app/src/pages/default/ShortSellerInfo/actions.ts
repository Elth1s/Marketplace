import { Dispatch } from "react"

import http from "../../../http_comon"

import { ServerError } from "../../../store/types"
import { ShopInfoAction, ShopInfoActionTypes, IShopInfo } from "./types";

export const ShopInfoFromProduct = (id: number) => {
    return async (dispatch: Dispatch<ShopInfoAction>) => {
        try {
            let response = await http.get<IShopInfo>(`api/Shop/ShopInfoFromProduct/${id}`)
            
            dispatch({
                type: ShopInfoActionTypes.GET_SHOP_INFO,
                payload: response.data
            })
            
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}