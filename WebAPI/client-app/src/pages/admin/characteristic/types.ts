export enum CharacteristicActionTypes {
    GET_CHARACTERISTICS = "GET_CHARACTERISTICS",
    GET_BY_ID_CHARACTERISTIC = "GET_BY_ID_CHARACTERISTIC",
    CREATE_CHARACTERISTIC = "CREATE_CHARACTERISTIC",
    UPDATE_CHARACTERISTIC = "UPDATE_CHARACTERISTIC",
}

export interface ICharacteristic {
    name: string,
    characteristicGroupId: number,
}

export interface ICharacteristicInfo {
    id: number,
    name: string,
    characteristicGroupName: string,
}

export interface CharacteristicState {
    characteristicInfo: ICharacteristicInfo,
    characteristics: Array<ICharacteristicInfo>
}


export interface GetCharacteristicsAction {
    type: CharacteristicActionTypes.GET_CHARACTERISTICS,
    payload: Array<ICharacteristicInfo>
}

export interface GetByIdCharacteristicAction {
    type: CharacteristicActionTypes.GET_BY_ID_CHARACTERISTIC,
    payload: ICharacteristicInfo
}

export interface CreateCharacteristicAction {
    type: CharacteristicActionTypes.CREATE_CHARACTERISTIC,
    payload: ICharacteristic
}

export interface UpdateCharacteristicAction {
    type: CharacteristicActionTypes.UPDATE_CHARACTERISTIC,
    payload: ICharacteristic
}


export type CharacteristicAction = GetCharacteristicsAction |
    GetByIdCharacteristicAction |
    CreateCharacteristicAction |
    UpdateCharacteristicAction;