import { CategoryAction, CategoryActionTypes, CategoryState } from "./types";

const initialState: CategoryState = {
    selectedCategory: {
        id: 0,
        englishName: "",
        ukrainianName: "",
        urlSlug: "",
        image: "",
        icon: "",
        parentId: 0
    },
    categories: [],
    categoriesForSelect: [],
    count: 0,
}

export const categoryReducer = (state = initialState, action: CategoryAction): CategoryState => {
    switch (action.type) {
        case CategoryActionTypes.GET_CATEGORIES:
            return {
                ...state,
                categories: action.payload,
            }
        case CategoryActionTypes.SEARCH_CATEGORIES:
            return {
                ...state,
                categories: action.payload.values,
                count: action.payload.count,
            }
        case CategoryActionTypes.GET_CATEGORIES_FOR_SELECT:
            return {
                ...state,
                categoriesForSelect: action.payload,
            }
        case CategoryActionTypes.GET_BY_ID_CATEGORY:
            return {
                ...state,
                selectedCategory: action.payload,
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