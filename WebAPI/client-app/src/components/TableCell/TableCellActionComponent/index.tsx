import TableCell from "@mui/material/TableCell"

import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

import { FC } from "react"
import { ITableCellAction } from "./type"

const TableCellActionComponent: FC<ITableCellAction> =
    ({ path, edit, onDelete }) => {
        return (
            <TableCell>
                {path === null ? (
                    edit
                ) : (
                    <IconButton
                        aria-label="edit"
                        href={path}>
                        <EditIcon />
                    </IconButton>
                )}
                <IconButton
                    aria-label="delete"
                    onClick={onDelete}>
                    <DeleteIcon />
                </IconButton>
            </TableCell>
        )
    }

export default TableCellActionComponent;