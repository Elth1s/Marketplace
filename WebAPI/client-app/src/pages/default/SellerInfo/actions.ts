import { Dispatch } from "react";
import { IShopPageInfo, IShopReview, IShopReviewWithCount, ShopPageAction, ShopPageActionTypes, ShopReviewAction, ShopReviewActionTypes } from "./types";
import http from "../../../http_comon"
import qs from "qs";
import { ServerError } from "../../../store/types"


export const GetShopInfo = (shopId: string,) => {
    return async (dispatch: Dispatch<ShopPageAction>) => {
        try {
            let response = await http.get<IShopPageInfo>(`api/Shop/GetShopInfo/${shopId}`);

            dispatch({
                type: ShopPageActionTypes.GET_SHOP_PAGE_INFO,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetShopReviews = (shopId: string, page: number, rowsPerPage: number) => {
    return async (dispatch: Dispatch<ShopReviewAction>) => {
        try {
            let response = await http.get<IShopReviewWithCount>(`api/ShopReview/GetForShop`, {
                params: {
                    shopId: shopId,
                    page: page,
                    rowsPerPage: rowsPerPage
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: ShopReviewActionTypes.GET_SHOP_REVIEWS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}


export const GetMoreShopReviews = (shopId: string, page: number, rowsPerPage: number) => {
    return async (dispatch: Dispatch<ShopReviewAction>) => {
        try {
            let response = await http.get<IShopReviewWithCount>(`api/ShopReview/GetForShop`, {
                params: {
                    shopId: shopId,
                    page: page,
                    rowsPerPage: rowsPerPage
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });
            dispatch({
                type: ShopReviewActionTypes.GET_MORE_SHOP_REVIEWS,
                payload: response.data.values
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const AddShopReview = (values: IShopReview) => {
    return async () => {
        try {
            await http.post(`api/ShopReview/Create`, values);

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}
