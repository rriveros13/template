import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { DetallePagosProveedorComponent } from './detalle-pagos-proveedor.component';

describe('DetallePagosProveedorComponent', () => {
  let component: DetallePagosProveedorComponent;
  let fixture: ComponentFixture<DetallePagosProveedorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ DetallePagosProveedorComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(DetallePagosProveedorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
