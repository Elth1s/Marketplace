import { ShopInfoAction, ShopInfoActionTypes, ShopInfoState} from "./types";

const initialState: ShopInfoState = {
    shopInfo: {
        name: "",
        photo: "",
        email: "",
        siteUrl: "",
        adress: "",
        phones: []
    },
}

export const shopInfoReducer = (state = initialState, action: ShopInfoAction): ShopInfoState => {
    switch (action.type) {
        case ShopInfoActionTypes.GET_SHOP_INFO:
            return {
                ...state,
                shopInfo: action.payload,
            }
        default:
            return state;
    }
}