import { CategoryAction, CategoryActionTypes, CategoryState } from "./types";

const initialState: CategoryState = {
    categoryInfo: {
        id: 0,
        name: "",
        image: "",
        parentId: undefined,
        characteristicId: 0
    },
    categories: [],
    categoriesForSelect: [],
    last_page: 0,
}

export const categoryReducer = (state = initialState, action: CategoryAction): CategoryState => {
    switch (action.type) {

        case CategoryActionTypes.GET_CATEGORIES:
            return {
                ...state,
                categories: action.payload,
            }
        case CategoryActionTypes.GET_CATEGORIES_FOR_SELECT:
            return {
                ...state,
                categoriesForSelect: action.payload,
            }
        case CategoryActionTypes.GET_BY_ID_CATEGORY:
            return {
                ...state,
                categoryInfo: action.payload,
            }
        case CategoryActionTypes.CREATE_CATEGOTY:
            return {
                ...state,
                ...action.payload
            }
        case CategoryActionTypes.UPDATE_CATEGORY:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}