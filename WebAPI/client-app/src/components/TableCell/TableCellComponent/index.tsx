import TableCell from "@mui/material/TableCell"

import { FC } from "react"
import { ITableCell } from "./type"

const TableCellComponent: FC<ITableCell> = ({ label }) => {
    return (
        <TableCell>{label}</TableCell>
    )
}

export default TableCellComponent;