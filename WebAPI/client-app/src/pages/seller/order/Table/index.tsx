import { Box, Typography, TableRow, Checkbox } from '@mui/material';

import { useEffect, useState } from "react";
import { useTranslation } from 'react-i18next';

import { HeadCell } from '../../../../store/types';

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import EnhancedTable from '../../../../components/EnhancedTable';

import { IOrderInfo } from '../type';
import { TableCellStyle } from '../../../../components/EnhancedTable/styled';
import Update from '../Update';

const OrdersTable = () => {
    const { t } = useTranslation();

    const headCells: HeadCell<IOrderInfo>[] = [
        {
            id: 'id',
            numeric: true,
            label: `${t('containers.admin_seller.tableHeadCell.identifier')}`,
        },
        {
            id: 'orderStatusName',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.orderStatus')}`,
        },
        {
            id: 'consumerFirstName',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.firstName')}`,
        },
        {
            id: 'consumerSecondName',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.secondName')}`,
        },
        {
            id: 'consumerPhone',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.phone')}`,
        },
        {
            id: 'city',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.city')}`,
        },
        {
            id: 'department',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.department')}`,
        },
        {
            id: 'totalPrice',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.totalPrice')}`,
        },
        {
            id: 'deliveryType',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.deliveryType')}`,
        },
    ];

    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(8);
    const [name, setName] = useState("");
    const [isAscOrder, setIsAscOrder] = useState<boolean>(true);
    const [orderBy, setOrderBy] = useState<keyof IOrderInfo>('id');

    const [selected, setSelected] = useState<readonly number[]>([]);

    const { AdminSellerSearchOrders } = useActions();
    const { orders, count } = useTypedSelector((store) => store.order);

    useEffect(() => {
        getData();
        document.title = `${t('containers.admin_seller.sideBar.order')}`;
    }, [page, rowsPerPage, name, isAscOrder, orderBy]);

    const getData = async () => {
        try {
            await AdminSellerSearchOrders(page, rowsPerPage, name, isAscOrder, orderBy);
            setSelected([]);
        } catch (ex) {
        }
    };

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
            const newSelecteds = orders.map((n) => n.id);
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
            <Typography variant="h1" color="inherit" sx={{ my: "30px", py: "4.5px" }}>{t('containers.admin_seller.sideBar.order')}</Typography>
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
                isDelete={false}
                onDelete={() => { }}
                update={<Update id={selected[selected.length - 1]} afterUpdate={() => { getData() }} />}
                tableBody={
                    orders.map((row, index) => {
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
                                <TableCellStyle align="center">{row.orderStatusName}</TableCellStyle>
                                <TableCellStyle align="center">{row.consumerFirstName}</TableCellStyle>
                                <TableCellStyle align="center">{row.consumerSecondName}</TableCellStyle>
                                <TableCellStyle align="center">{row.consumerPhone}</TableCellStyle>
                                <TableCellStyle align="center">{row.city}</TableCellStyle>
                                <TableCellStyle align="center">{row.department}</TableCellStyle>
                                <TableCellStyle align="center">{row.totalPrice} {t("currency")}</TableCellStyle>
                                <TableCellStyle align="center">{row.deliveryType}</TableCellStyle>
                            </TableRow>
                        );
                    })
                }
            />
        </>
    );
}

export default OrdersTable;