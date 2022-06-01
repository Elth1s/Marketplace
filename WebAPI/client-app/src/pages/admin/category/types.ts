export enum CategoryActionTypes {
    GET_CATEGORIES = "GET_CATEGORIES",
    GET_CATEGORIES_FOR_SELECT = "GET_CATEGORIES_FOR_SELECT",
    GET_BY_ID_CATEGORY = "GET_BY_ID_CATEGORY",
    CREATE_CATEGOTY = "CREATE_CATEGOTY",
    UPDATE_CATEGORY = "UPDATE_CATEGORY",
}

export interface ICategory {
    name: string,
    image: string,
    parentId: number | null
}

export interface ICategoryInfo {
    id: number
    name: string,
    image: string,
    parentName: string
}

export interface ICategoryForSelect {
    id: number
    name: string,
}


export interface CategoryState {
    categoryInfo: ICategoryInfo,
    categories: Array<ICategoryInfo>,
    categoriesForSelect: Array<ICategoryForSelect>,
    last_page: number
}

export interface CategoryServerError {
    title: string,
    status: number,
    errors: Array<any>,
}


export interface GetCategoriesAction {
    type: CategoryActionTypes.GET_CATEGORIES,
    payload: Array<ICategoryInfo>
}

export interface GetCategoriesForSelectAction {
    type: CategoryActionTypes.GET_CATEGORIES_FOR_SELECT,
    payload: Array<ICategoryForSelect> 
}

export interface GetByIdCategoryAction {
    type: CategoryActionTypes.GET_BY_ID_CATEGORY,
    payload: ICategoryInfo
}

export interface CreateCategoryAction {
    type: CategoryActionTypes.CREATE_CATEGOTY,
    payload: ICategory
}

export interface UpdateCategoryAction {
    type: CategoryActionTypes.UPDATE_CATEGORY,
    payload: ICategory
}


export type CategoryAction = GetCategoriesAction | GetCategoriesForSelectAction | GetByIdCategoryAction | CreateCategoryAction | UpdateCategoryAction;