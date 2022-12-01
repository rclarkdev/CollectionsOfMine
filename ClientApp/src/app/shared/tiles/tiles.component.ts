import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: "app-tiles",
  templateUrl: "./tiles.component.html",
  styleUrls: ["./tiles.component.css"],
})
export class TilesComponent implements OnInit {
  private fields: string[];
  @Input() tiles: any[];
  @Input() tile: string;

  constructor(private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.fields = Object.keys(this.tiles[0]);
  }

  selectTile(tile) {
    this.router.navigate([this.tile + "/" + tile['id']]);
  }
}
