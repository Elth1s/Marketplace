import { SaleAction, SaleActionTypes, SaleState } from "./types";

const initialState: SaleState = {
    selectedSale: {
        name: "",
        ukrainianVerticalImage: "",
        ukrainianHorizontalImage: "",
        englishHorizontalImage: "",
        englishVerticalImage: "",
        discountMin: 0,
        discountMax: 0,
        dateStart: "",
        dateEnd: ""
    },
    sales: [],
    count: 0,
}

export const saleReducer = (state = initialState, action: SaleAction): SaleState => {
    switch (action.type) {
        case SaleActionTypes.GET_SALES:
            return {
                ...state,
                sales: action.payload,
            }
        case SaleActionTypes.SEARCH_SALES:
            return {
                ...state,
                sales: action.payload.values,
                count: action.payload.count,
            }
        case SaleActionTypes.GET_BY_ID_SALE:
            return {
                ...state,
                selectedSale: action.payload,
            }
        case SaleActionTypes.CREATE_SALE:
            return {
                ...state,
                ...action.payload
            }
        case SaleActionTypes.UPDATE_SALE:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}