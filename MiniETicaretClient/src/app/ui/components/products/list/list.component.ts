import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseUrl } from 'src/app/contracts/files/base_url';
import { List_Product } from 'src/app/contracts/list_product';
import { FileService } from 'src/app/services/common/models/file.service';
import { ProductService } from 'src/app/services/common/models/product.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
})
export class ListComponent implements OnInit {
  constructor(
    private productService: ProductService,
    private activatedRoute: ActivatedRoute,
    private fileService: FileService
  ) {}

  products: List_Product[];
  currentPageNo: number;
  currentPageSize: number = 12;
  totalProductCount: number;
  totalPageCount: number;
  pageNoList: number[] = [];
  baseUrl: BaseUrl;

  async ngOnInit() {
    this.baseUrl = (await this.fileService.getBaseStorageUrl());
    this.activatedRoute.params.subscribe(async (params) => {
      this.currentPageNo = parseInt(params['pageNo'] ?? 1);
      const data: { totalCount: number; products: List_Product[] } =
        await this.productService.read(
          this.currentPageNo - 1,
          this.currentPageSize,
          () => {},
          (errorMessage) => {}
        );
      this.products = data.products;

      this.products = this.products.map<List_Product>((product) => {
        const listProduct: List_Product = {
          id: product.id,
          name: product.name,
          price: product.price,
          stock: product.stock,
          updatedDate: product.updatedDate,
          createdDate: product.createdDate,
          imagePath: product.productImageFiles.length
            ? product.productImageFiles.find((p) => p.showcase).path
            : '',
          productImageFiles: product.productImageFiles,
        };

        return listProduct;
      });

      this.totalProductCount = data.totalCount;
      this.totalPageCount = Math.ceil(
        this.totalProductCount / this.currentPageSize
      );

      this.pageNoList = [];

      if (this.totalPageCount >= 7) {
        if (this.currentPageNo - 3 <= 0)
          for (let i = 1; i <= 7; i++) this.pageNoList.push(i);
        else if (this.currentPageNo + 3 >= this.totalPageCount)
          for (let i = this.totalPageCount - 6; i <= this.totalPageCount; i++)
            this.pageNoList.push(i);
        else
          for (let i = this.currentPageNo - 3; i <= this.currentPageNo + 3; i++)
            this.pageNoList.push(i);
      } else {
        for (let i = 1; i <= this.totalPageCount; i++) {
          this.pageNoList.push(i);
        }
      }
    });
  }
}
