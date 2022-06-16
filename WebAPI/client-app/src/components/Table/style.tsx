import Checkbox from "@mui/material/Checkbox";
import TablePagination from "@mui/material/TablePagination";
import TableCell from "@mui/material/TableCell";
import TableSortLabel from "@mui/material/TableSortLabel";

import { styled } from '@mui/material/styles';


export const TableCellStyle = styled(TableCell)(({ theme }) => ({
    "&.MuiTableCell-root": {
        color: "#000000",
    }
}));

export const TableSortLabelStyle = styled(TableSortLabel)(({ theme }) => ({
    "&.MuiTableSortLabel-root": {
        "&:hover": {
            color: "#000000",
            "& .MuiTableSortLabel-icon": {
                opacity: "1",
            },
        },
    },
    "&.Mui-active": {
        color: "#000000",
        "& .MuiTableSortLabel-icon": {
            color: "#000000",
            opacity: "1",
        },
    },
    "& .MuiTableSortLabel-icon": {
        opacity: "1",
        "&:hover": {
            opacity: "1",
        },
    },
}))

export const CheckboxStyle = styled(Checkbox)(({ theme }) => ({
    "&.MuiCheckbox-root": {
        color: "#7D7D7D",
        "&:hover": {
            backgroundColor: "transparent",
        },
        "&.Mui-checked": {
            color: "#FDA906",
        },
        "&.MuiCheckbox-indeterminate": {
            color: "#FDA906",
        },
    },
}));

export const TablePaginationStyle = styled(TablePagination)(({ theme }) => ({
    "& .MuiTablePagination-select": {
        color: "#000000",
        borderRadius: "28px",
        border: "1px solid #AEAEAE",
        "&:focus": {
            borderRadius: "28px",
            border: "1px solid #AEAEAE",
        }
    },
}));
