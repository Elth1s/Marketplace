import {
    ShopAction,
    ShopActionTypes,
    ShopState
} from "./types";

const initialState: ShopState = {
    selectedShop: {
        name: "",
        description: "",
        email: "",
        phones: [],
        photo: "",
        siteUrl: "",
        countryName: "",
        cityName: "",
        userFullName: ""
    },
    count: 0,
    shops: []
}

export const shopReducer = (state = initialState, action: ShopAction): ShopState => {
    switch (action.type) {
        case ShopActionTypes.GET_SHOPS:
            return {
                ...state,
                shops: action.payload,
            }
        case ShopActionTypes.SEARCH_SHOPS:
            return {
                ...state,
                count: action.payload.count,
                shops: action.payload.values,
            }
        case ShopActionTypes.GET_BY_ID_SHOP:
            return {
                ...state,
                selectedShop: action.payload,
            }
        default:
            return state;
    }
}