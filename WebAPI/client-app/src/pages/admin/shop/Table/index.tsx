import { Avatar, Checkbox, TableRow, Typography } from '@mui/material';
import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useActions } from '../../../../hooks/useActions';
import { useTypedSelector } from '../../../../hooks/useTypedSelector';

import ShowShop from '../Show';
import { IShopInfo } from '../types';
import { HeadCell } from '../../../../store/types';

import EnhancedTable from '../../../../components/EnhancedTable';
import { TableCellStyle } from '../../../../components/EnhancedTable/styled';


const ShopTable = () => {
    const { t } = useTranslation();

    const headCells: HeadCell<IShopInfo>[] = [
        {
            id: 'id',
            numeric: true,
            label: `${t('containers.admin_seller.tableHeadCell.identifier')}`,
        },
        {
            id: 'name',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.name')}`,
        },
        {
            id: 'photo',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.photo')}`,
        },
        {
            id: 'siteUrl',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.siteURL')}`,
        },
        {
            id: 'cityName',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.city')}`,
        }
    ];

    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(8);
    const [name, setName] = useState("");
    const [isAscOrder, setIsAscOrder] = useState<boolean>(true);
    const [orderBy, setOrderBy] = useState<keyof IShopInfo>('id');

    const [selected, setSelected] = useState<readonly number[]>([]);

    const { SearchShops, DeleteShops } = useActions();
    const { shops, count } = useTypedSelector((store) => store.shop);

    useEffect(() => {
        document.title = `${t('containers.admin_seller.sideBar.shops')}`;
        getData();
    }, [page, rowsPerPage, name, isAscOrder, orderBy]);

    const getData = async () => {
        try {
            await SearchShops(page, rowsPerPage, name, isAscOrder, orderBy);
            setSelected([]);
        } catch (ex) {
        }
    };

    const onDelete = async () => {
        await DeleteShops(selected);
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
            const newSelecteds = shops.map((n) => n.id);
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
            <Typography variant="h1" color="inherit" sx={{ my: "30px", py: "4.5px" }}>{t('containers.admin_seller.sideBar.shops')}</Typography>
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
                show={<ShowShop id={selected[selected.length - 1]} />}
                update={null}
                onDelete={onDelete}
                tableBody={shops.map((row, index) => {
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
                                    }} />
                            </TableCellStyle>
                            <TableCellStyle
                                component="th"
                                id={labelId}
                                scope="row"
                            >
                                {row.id}
                            </TableCellStyle>
                            <TableCellStyle align="center">{row.name}</TableCellStyle>
                            <TableCellStyle align="center">
                                <Avatar
                                    sx={{ mx: "auto" }}
                                    src={row.photo}
                                    alt="Photo"
                                /></TableCellStyle>
                            <TableCellStyle align="center">{row.siteUrl}</TableCellStyle>
                            <TableCellStyle align="center">{row.cityName}</TableCellStyle>
                        </TableRow>
                    );
                })} />
        </>
    );
}

export default ShopTable