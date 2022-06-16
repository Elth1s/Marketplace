export interface ServerError {
    title: string,
    status: number,
    errors: Array<any>,
}