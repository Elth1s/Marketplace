import { ReactElement } from "react"

export interface ITableCellAction {
    path: string | null,
    edit: ReactElement<any, any> | null,
    onDelete: () => Promise<void>,
}