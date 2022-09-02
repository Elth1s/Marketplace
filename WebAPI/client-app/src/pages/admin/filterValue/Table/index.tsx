import { Box, Typography, Checkbox, TableRow } from '@mui/material';

import { useEffect, useState } from "react";
import { useTranslation } from 'react-i18next';

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import EnhancedTable from '../../../../components/EnhancedTable';
import { TableCellStyle } from '../../../../components/EnhancedTable/styled';

import { IFilterValueInfo } from '../types';
import { HeadCell } from '../../../../store/types';
import Create from '../Create';
import Update from '../Update';

const FilterValueTable = () => {
    const { t } = useTranslation();

    const headCells: HeadCell<IFilterValueInfo>[] = [
        {
            id: 'id',
            numeric: true,
            label: `${t('containers.admin_seller.tableHeadCell.identifier')}`,
        },
        {
            id: 'value',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.value')}`,
        },
        {
            id: 'min',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.min')}`,
        },
        {
            id: 'max',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.max')}`,
        },
        {
            id: 'filterName',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.filterName')}`,
        },
    ];

    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(8);
    const [name, setName] = useState("");
    const [isAscOrder, setIsAscOrder] = useState<boolean>(true);
    const [orderBy, setOrderBy] = useState<keyof IFilterValueInfo>('id');

    const [selected, setSelected] = useState<readonly number[]>([]);

    const { SearchFilterValues, DeleteFilterValues } = useActions();
    const { filterValues, count } = useTypedSelector((store) => store.filterValue);

    useEffect(() => {
        getData();
        document.title = `${t('containers.admin_seller.sideBar.filterValues')}`;
    }, [page, rowsPerPage, name, isAscOrder, orderBy]);

    const getData = async () => {
        try {
            await SearchFilterValues(page, rowsPerPage, name, isAscOrder, orderBy);
            setSelected([]);
        } catch (ex) {
        }
    };

    const onDelete = async () => {
        await DeleteFilterValues(selected);
        if (page == 1)
            getData()
        else
            await setPage(1);
    }

    const handleClick = (event: React.MouseEvent<unknown>, id: number) => {
        const selectedIndex = selected.indexOf(id);
        let newSelected: readonly number[] = [];

        if (selectedIndex === -1) {
            newSelected = newSelected.concat(selected, id);
        } else if (selectedIndex === 0) {
            newSelected = newSelected.concat(selected.slice(1));
        } else if (selectedIndex === selected.length - 1) {
            newSelected = newSelected.concat(selected.slice(0, -1));
        } else if (selectedIndex > 0) {
            newSelected = newSelected.concat(
                selected.slice(0, selectedIndex),
                selected.slice(selectedIndex + 1),
            );
        }

        setSelected(newSelected);
    };

    const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (selected.length == 0) {
            const newSelecteds = filterValues.map((n) => n.id);
            setSelected(newSelecteds);
            return;
        }
        setSelected([]);
    };

    const isSelected = (id: number) => selected.indexOf(id) !== -1;

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(1);
    };

    return (
        <>
            <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", my: "30px" }}>
                <Typography variant="h1">{t('containers.admin_seller.sideBar.filterValues')}</Typography>
                <Create afterCreate={() => { getData() }} />
            </Box>
            <EnhancedTable
                page={page}
                rowsPerPage={rowsPerPage}
                handleChangePage={handleChangePage}
                handleChangeRowsPerPage={handleChangeRowsPerPage}
                setName={setName}
                handleSelectAllClick={handleSelectAllClick}
                isAscOrder={isAscOrder}
                setIsAscOrder={setIsAscOrder}
                orderBy={orderBy}
                setOrderBy={setOrderBy}
                headCells={headCells}
                numSelected={selected.length}
                count={count}
                onDelete={onDelete}
                update={<Update id={selected[selected.length - 1]} afterUpdate={() => { getData() }} />}
                tableBody={
                    filterValues.map((row, index) => {
                        const isItemSelected = isSelected(row.id);
                        const labelId = `enhanced-table-checkbox-${index}`;

                        return (
                            <TableRow
                                hover
                                onClick={(event) => handleClick(event, row.id)}
                                role="checkbox"
                                aria-checked={isItemSelected}
                                tabIndex={-1}
                                key={row.id}
                                selected={isItemSelected}
                            >
                                <TableCellStyle sx={{ paddingLeft: "11px" }} padding="checkbox">
                                    <Checkbox
                                        color="primary"
                                        sx={{ borderRadius: "12px" }}
                                        checked={isItemSelected}
                                        inputProps={{
                                            'aria-labelledby': labelId,
                                        }}
                                    />
                                </TableCellStyle>
                                <TableCellStyle
                                    component="th"
                                    id={labelId}
                                    scope="row"
                                >
                                    {row.id}
                                </TableCellStyle>
                                <TableCellStyle align="center">{row.value}</TableCellStyle>
                                <TableCellStyle align="center">{row.min}</TableCellStyle>
                                <TableCellStyle align="center">{row.max}</TableCellStyle>
                                <TableCellStyle align="center">{row.filterName}</TableCellStyle>
                            </TableRow>
                        );
                    })
                }
            />
        </>
    );
}

export default FilterValueTable;