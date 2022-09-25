import {
    Box,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    TableSortLabel,
    Toolbar,
    Checkbox,
    IconButton,
    Tooltip,
    InputAdornment,
    Pagination,
    MenuItem,
    Typography
} from '@mui/material';
import { Delete, Search } from '@mui/icons-material';
import { alpha } from '@mui/material/styles';
import { visuallyHidden } from '@mui/utils';
import { FC } from 'react';

import { PaperStyle, TextFieldStyle, PaginationItemStyle, SelectStyle } from './styled';
import { useTranslation } from 'react-i18next';

interface EnhancedTableHeadProps {
    numSelected: number;
    onRequestSort: (event: React.MouseEvent<unknown>, property: string) => void;
    onSelectAllClick: (event: React.ChangeEvent<HTMLInputElement>) => void;
    isAscOrder: boolean;
    orderBy: string;
    rowCount: number;
    headCells: any[];
}

function EnhancedTableHead(props: EnhancedTableHeadProps) {
    const { onSelectAllClick, isAscOrder, orderBy, numSelected, rowCount, onRequestSort, headCells } =
        props;
    const createSortHandler =
        (property: string) => (event: React.MouseEvent<unknown>) => {
            onRequestSort(event, property);
        };

    return (
        <TableHead >
            <TableRow >
                <TableCell sx={{ borderColor: "#7D7D7D", paddingLeft: "11px" }} padding="checkbox">
                    <Checkbox
                        color="primary"
                        sx={{ borderRadius: "12px" }}
                        indeterminate={numSelected > 0 && numSelected < rowCount}
                        checked={rowCount > 0 && numSelected === rowCount}
                        onChange={onSelectAllClick}
                        inputProps={{
                            'aria-label': 'select all desserts',
                        }}
                    />
                </TableCell>
                {headCells.map((headCell) => (
                    <TableCell
                        sx={{ borderColor: "#7D7D7D", px: "8px" }}
                        key={headCell.id}
                        align={headCell.numeric ? 'left' : 'center'}
                        sortDirection={orderBy === headCell.id ? (isAscOrder ? "asc" : "desc") : false}
                    >
                        <TableSortLabel
                            active={orderBy === headCell.id}
                            direction={orderBy === headCell.id ? (isAscOrder ? "asc" : "desc") : 'asc'}
                            onClick={createSortHandler(headCell.id)}
                        >
                            {headCell.label}
                            {orderBy === headCell.id ? (
                                <Box component="span" sx={visuallyHidden}>
                                    {(isAscOrder ? "asc" : "desc") === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                </Box>
                            ) : null}
                        </TableSortLabel>
                    </TableCell>
                ))}

            </TableRow>
        </TableHead>
    );
}

interface EnhancedTableToolbarProps {
    numSelected: number;
    onChangeSearch: any;
    onPageChange: any;
    onDelete: any;
    update: any;
    show: any
}

const EnhancedTableToolbar = (props: EnhancedTableToolbarProps) => {
    const { numSelected, onChangeSearch, onPageChange, update, show, onDelete } = props;
    const { t } = useTranslation();
    return (
        <Toolbar
            sx={{
                borderTopRightRadius: "32px",
                borderTopLeftRadius: "32px",
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
                ...(numSelected > 0 && {
                    bgcolor: (theme) =>
                        alpha(theme.palette.primary.main, theme.palette.action.activatedOpacity),
                }),
            }}
        >
            <TextFieldStyle placeholder={t('containers.admin_seller.table.search')} onChange={(event => { onChangeSearch(event.target.value); onPageChange(event, 1) })}
                InputProps={{
                    startAdornment: (
                        <InputAdornment position="start" >
                            <Search sx={{ width: "35px", height: "35px" }} />
                        </InputAdornment>
                    )
                }} />
            <Box sx={{ width: "70%", display: "flex", alignItems: "center", justifyContent: "end", px: 1 }}>
                {update != null &&
                    <IconButton size="medium"
                        sx={{
                            borderRadius: "12px",
                            ...(numSelected != 1 && {
                                display: "none",
                            }),
                        }}>
                        {update}
                    </IconButton>
                }
                {show != null &&
                    <IconButton size="medium"
                        sx={{
                            borderRadius: "12px",
                            ...(numSelected != 1 && {
                                display: "none",
                            }),
                        }}>
                        {show}
                    </IconButton>}
                <IconButton size="medium"
                    onClick={onDelete}
                    sx={{
                        borderRadius: "12px",
                        ...(numSelected == 0 && {
                            display: "none",
                        }),
                    }}>
                    <Delete />
                </IconButton>
            </Box>

        </Toolbar >
    );
};

interface FooterTableToolbarProps {
    rowsPerPageOptions: number[],
    page: number,
    rowsPerPage: number,
    count: number,
    onPageChange: any,
    onRowsPerPageChange: any
}

const FooterTableToolbar = (props: FooterTableToolbarProps) => {
    const { t } = useTranslation();
    const { rowsPerPageOptions, page, rowsPerPage, count, onPageChange, onRowsPerPageChange } = props;

    return (
        <Toolbar
            sx={{
                borderBootomRightRadius: "32px",
                borderBootomLeftRadius: "32px",
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
            }}
        >
            <Box sx={{ display: "flex", alignItems: "center" }}>
                <SelectStyle
                    value={rowsPerPage}
                    onChange={onRowsPerPageChange}
                    autoWidth
                >
                    {rowsPerPageOptions.map((element, index) => (
                        <MenuItem key={`${element}`} value={element}>{element}</MenuItem>
                    ))}
                </SelectStyle>
                <Typography color="inherit" sx={{ px: 1 }}>{t('containers.admin_seller.table.rowsPerPage')} {((page - 1) * rowsPerPage) + 1}-{page * rowsPerPage} {t('containers.admin_seller.table.of')} {count}</Typography>
            </Box>
            <Pagination count={Math.ceil(count / rowsPerPage)} page={page} shape="rounded" size="large" showFirstButton showLastButton
                onChange={(event: any, value: number) => {
                    onPageChange(event, value);
                }}
                renderItem={(item) => <PaginationItemStyle {...item} />} />

        </Toolbar >
    );
};

interface EnhancedTableProps {
    page: number,
    rowsPerPage: number,
    handleChangePage: any,
    handleChangeRowsPerPage: any,
    setName: any,

    isAscOrder: boolean,
    setIsAscOrder: any,
    orderBy: string,
    setOrderBy: any,

    handleSelectAllClick: any,

    headCells: any[],

    numSelected: number,

    count: number,

    onDelete: any,
    update: any | null,
    show?: any | null,

    tableBody: any
}

const EnhancedTable: FC<EnhancedTableProps> = ({ page, rowsPerPage, handleChangePage, handleChangeRowsPerPage, isAscOrder, setIsAscOrder, orderBy, setOrderBy, handleSelectAllClick, setName, headCells, numSelected, count, onDelete, update, show, tableBody }) => {

    const handleRequestSort = (
        event: React.MouseEvent<unknown>,
        property: string,
    ) => {
        const isAsc = orderBy === property && ((isAscOrder ? 'asc' : 'desc') === 'asc');
        setIsAscOrder(!isAsc);
        setOrderBy(property);
    };

    return (
        <Box sx={{ width: '100%' }}>
            <PaperStyle elevation={0}
                sx={{
                    marginBottom: 2,
                    border: 1,
                    borderColor: (theme) => `${theme.palette.mode == "dark" ? theme.palette.common.white : theme.palette.common.black}`,
                }}
            >
                <EnhancedTableToolbar numSelected={numSelected} onChangeSearch={setName} onPageChange={handleChangePage} update={update} show={show} onDelete={() => onDelete()} />
                <TableContainer>
                    <Table
                        sx={{ minWidth: 750 }}
                        aria-labelledby="tableTitle"
                        size={'medium'}
                    >
                        <EnhancedTableHead
                            numSelected={numSelected}
                            isAscOrder={isAscOrder}
                            orderBy={orderBy}
                            onSelectAllClick={handleSelectAllClick}
                            onRequestSort={handleRequestSort}
                            rowCount={count}
                            headCells={headCells}
                        />
                        <TableBody sx={{ paddingLeft: 3 }}>
                            {tableBody}
                        </TableBody>
                    </Table>
                </TableContainer>
                <FooterTableToolbar rowsPerPageOptions={[8, 16, 24]} page={page} rowsPerPage={rowsPerPage} count={count} onPageChange={handleChangePage} onRowsPerPageChange={handleChangeRowsPerPage} />
            </PaperStyle>
        </Box>
    );
}

export default EnhancedTable;