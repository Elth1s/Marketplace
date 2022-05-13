export enum CategoryActionTypes {
    GET_CATEGORIES = "GET_CATEGORIES",
    GET_CATEGORIES_FOR_SELECT = "GET_CATEGORIES_FOR_SELECT",
    GET_BY_ID_CATEGORY = "GET_BY_ID_CATEGORY",
    CREATE_CATEGOTY = "CREATE_CATEGOTY",
    UPDATE_CATEGORY = "UPDATE_CATEGORY",
    DELETE_CATEGORY = "DELETE_CATEGORY",
}

export interface ICategory {
    id: number
    name: string,
    image: string,
    parentId: number | undefined,
    characteristicId: number
}
export interface ICreateCategory {
    name: string,
    image: string,
    parentId: number | undefined,
    characteristicId: number
}

export interface ICategoryForSelect {
    id: number
    name: string,
}


export interface CategoryState {
    categoryInfo: ICategory,
    categories: Array<ICategory>,
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
    payload: Array<ICategory>
}

export interface GetCategoriesForSelectAction {
    type: CategoryActionTypes.GET_CATEGORIES_FOR_SELECT,
    payload: Array<ICategoryForSelect> 
}

export interface GetByIdCategoryAction {
    type: CategoryActionTypes.GET_BY_ID_CATEGORY,
    payload: ICategory
}

export interface CreateCategoryAction {
    type: CategoryActionTypes.CREATE_CATEGOTY,
    payload: ICreateCategory
}

export interface UpdateCategoryAction {
    type: CategoryActionTypes.UPDATE_CATEGORY,
    payload: ICategory
}


export type CategoryAction = GetCategoriesAction | GetCategoriesForSelectAction | GetByIdCategoryAction | CreateCategoryAction | UpdateCategoryAction;