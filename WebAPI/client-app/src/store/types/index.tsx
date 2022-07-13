export interface ServerError {
    title: string,
    status: number,
    errors: Array<any>,
}

export interface CreateProps {
    afterCreate: any
}

export interface UpdateProps {
    id: number,
    afterUpdate: any
}

export interface ShowProps {
    id: number,
}

export interface HeadCell<T> {
    id: keyof T;
    numeric: boolean;
    label: string;
}