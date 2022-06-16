import { useTheme } from "@mui/material/styles"
import { TablePaginationActionsProps } from "@mui/material/TablePagination/TablePaginationActions"

import Box from "@mui/material/Box"
import IconButton from "@mui/material/IconButton"

import TableContainer from "@mui/material/TableContainer"
import Table from "@mui/material/Table"
import TableHead from "@mui/material/TableHead"
import TableBody from "@mui/material/TableBody"
import TableFooter from "@mui/material/TableFooter"
import TableRow from "@mui/material/TableRow"
import TableCell from "@mui/material/TableCell"
import TableSortLabel from "@mui/material/TableSortLabel"
import TablePagination from "@mui/material/TablePagination"
import Select from "@mui/material/Select"

import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
import FirstPageIcon from '@mui/icons-material/FirstPage';
import KeyboardArrowLeft from '@mui/icons-material/KeyboardArrowLeft';
import KeyboardArrowRight from '@mui/icons-material/KeyboardArrowRight';
import LastPageIcon from '@mui/icons-material/LastPage';

import { useState, FC, SetStateAction, ChangeEvent } from "react";

import { CheckboxStyle, TablePaginationStyle, TableCellStyle, TableSortLabelStyle } from "./style"
import { ITable } from "./type";

const TableComponent: FC<ITable> = ({
    headLabel,
    itemsCount,
    bodyItems,
    rowsPerPageOptions
}) => {
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(5);

    const [selected, setSelected] = useState<Array<number>>([]);

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    const handleSelectAllClick = (event: { target: { checked: any } }) => {
        if (event.target.checked) {
            const newSelected = bodyItems.map((item) => item[0].key);
            setSelected(newSelected);
            return;
        }
        setSelected([]);
    };

    const handleClick = (event: ChangeEvent<HTMLInputElement>, index: any) => {
        const selectedIndex = selected.indexOf(index);
        let newSelected: SetStateAction<any[]> = [];
        if (selectedIndex === -1) {
            newSelected = newSelected.concat(selected, index);
        } else if (selectedIndex === 0) {
            newSelected = newSelected.concat(selected.slice(1));
        } else if (selectedIndex === selected.length - 1) {
            newSelected = newSelected.concat(selected.slice(0, -1));
        } else if (selectedIndex > 0) {
            newSelected = newSelected.concat(selected.slice(0, selectedIndex), selected.slice(selectedIndex + 1));
        }
        setSelected(newSelected);
    };

    const TablePaginationActions = (props: TablePaginationActionsProps) => {
        const theme = useTheme();
        const { count, page, rowsPerPage, onPageChange } = props;

        const handleFirstPageButtonClick = (
            event: React.MouseEvent<HTMLButtonElement>,
        ) => {
            onPageChange(event, 0);
        };

        const handleBackButtonClick = (event: React.MouseEvent<HTMLButtonElement>) => {
            onPageChange(event, page - 1);
        };

        const handleNextButtonClick = (event: React.MouseEvent<HTMLButtonElement>) => {
            onPageChange(event, page + 1);
        };

        const handleLastPageButtonClick = (event: React.MouseEvent<HTMLButtonElement>) => {
            onPageChange(event, Math.max(0, Math.ceil(count / rowsPerPage) - 1));
        };

        return (
            <Box sx={{ flexShrink: 0, ml: 2.5 }}>
                <IconButton
                    onClick={handleFirstPageButtonClick}
                    disabled={page === 0}
                    aria-label="first page"
                >
                    {theme.direction === 'rtl' ? <LastPageIcon /> : <FirstPageIcon />}
                </IconButton>
                <IconButton
                    onClick={handleBackButtonClick}
                    disabled={page === 0}
                    aria-label="previous page"
                >
                    {theme.direction === 'rtl' ? <KeyboardArrowRight /> : <KeyboardArrowLeft />}
                </IconButton>
                <IconButton
                    onClick={handleNextButtonClick}
                    disabled={page >= Math.ceil(count / rowsPerPage) - 1}
                    aria-label="next page"
                >
                    {theme.direction === 'rtl' ? <KeyboardArrowLeft /> : <KeyboardArrowRight />}
                </IconButton>
                <IconButton
                    onClick={handleLastPageButtonClick}
                    disabled={page >= Math.ceil(count / rowsPerPage) - 1}
                    aria-label="last page"
                >
                    {theme.direction === 'rtl' ? <FirstPageIcon /> : <LastPageIcon />}
                </IconButton>
            </Box>
        );
    }

    return (
        <>
            <TableContainer>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCellStyle
                                padding="checkbox">
                                <CheckboxStyle
                                    indeterminate={selected.length > 0 && selected.length < itemsCount}
                                    checked={itemsCount > 0 && selected.length === itemsCount}
                                    onChange={handleSelectAllClick}
                                />
                            </TableCellStyle>
                            {headLabel.map((headCell) => (
                                <TableCellStyle
                                    key={headCell}>
                                    <TableSortLabelStyle
                                        active={true}
                                        direction={'asc'}
                                        IconComponent={ArrowDropDownIcon}
                                    >
                                        {headCell}
                                    </TableSortLabelStyle>
                                </TableCellStyle>
                            ))}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {bodyItems
                            .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                            .map((item, index) => {
                                let isItemSelected: boolean = selected.indexOf(item[0].key) !== -1;
                                return (
                                    <TableRow
                                        key={index}
                                        tabIndex={-1}
                                        role="checkbox"
                                        selected={isItemSelected}
                                        aria-checked={isItemSelected}
                                    >
                                        <TableCellStyle
                                            padding="checkbox"
                                        >
                                            <CheckboxStyle
                                                checked={isItemSelected}
                                                onChange={(event) => handleClick(event, item[0].key)}
                                            />
                                        </TableCellStyle>
                                        {item}
                                    </TableRow>
                                )
                            })
                        }
                    </TableBody>

                    <TableFooter>
                        <TableRow>
                            <TablePaginationStyle
                                rowsPerPageOptions={rowsPerPageOptions}
                                //component={Select}
                                colSpan={3}
                                count={itemsCount}
                                rowsPerPage={rowsPerPage}
                                page={page}
                                SelectProps={{
                                    inputProps: {
                                        'aria-label': 'rows 111 page',
                                    },
                                    native: true,
                                }}
                                onPageChange={handleChangePage}
                                onRowsPerPageChange={handleChangeRowsPerPage}
                                ActionsComponent={TablePaginationActions}
                            />
                        </TableRow>
                    </TableFooter>
                </Table>
            </TableContainer>
        </>
    )
}

export default TableComponent;
