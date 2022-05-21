export enum CharacteristicGroupActionTypes {
    GET_CHARACTERISTIC_GROUPS = "GET_CHARACTERISTIC_GROUPS",
    GET_BY_ID_CHARACTERISTIC_GROUP = "GET_BY_ID_CHARACTERISTIC_GROUP",
    CREATE_CHARACTERISTIC_GROUP = "CREATE_CHARACTERISTIC_GROUP",
    UPDATE_CHARACTERISTIC_GROUP = "UPDATE_CHARACTERISTIC_GROUP",
}

export interface ICharacteristicGroup {
    name: string
}

export interface ICharacteristicGroupInfo {
    id: number
    name: string
}

export interface CharacteristicGroupState {
    characteristicGroupInfo: ICharacteristicGroupInfo,
    characteristicGroups: Array<ICharacteristicGroupInfo>
}

export interface CharacteristicGroupServerError {
    title: string,
    status: number,
    errors: Array<any>,
}


export interface GetCharacteristicGroupsAction {
    type: CharacteristicGroupActionTypes.GET_CHARACTERISTIC_GROUPS,
    payload: Array<ICharacteristicGroupInfo>
}

export interface GetByIdCharacteristicGroupAction {
    type: CharacteristicGroupActionTypes.GET_BY_ID_CHARACTERISTIC_GROUP,
    payload: ICharacteristicGroupInfo
}

export interface CreateCharacteristicGroupAction {
    type: CharacteristicGroupActionTypes.CREATE_CHARACTERISTIC_GROUP,
    payload: ICharacteristicGroup
}

export interface UpdateCharacteristicGroupAction {
    type: CharacteristicGroupActionTypes.UPDATE_CHARACTERISTIC_GROUP,
    payload: ICharacteristicGroup
}


export type CharacteristicGroupAction = GetCharacteristicGroupsAction | 
                                        GetByIdCharacteristicGroupAction | 
                                        CreateCharacteristicGroupAction |
                                        UpdateCharacteristicGroupAction;