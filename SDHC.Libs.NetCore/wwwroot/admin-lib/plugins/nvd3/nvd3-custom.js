function randomData(e, t) {
    var n = [], r = [ "circle", "cross", "triangle-up", "triangle-down", "diamond", "square" ], s = d3.random.normal();
    for (i = 0; i < e; i++) {
        n.push({
            key: "Group " + i,
            values: []
        });
        for (j = 0; j < t; j++) n[i].values.push({
            x: s(),
            y: s(),
            size: Math.random(),
            shape: r[j % 6]
        });
    }
    return n;
}

function stream_layers(e, t, n) {
    function r(e) {
        var n = 1 / (.1 + Math.random()), r = 2 * Math.random() - .5, i = 10 / (.1 + Math.random());
        for (var s = 0; s < t; s++) {
            var o = (s / t - r) * i;
            e[s] += n * Math.exp(-o * o);
        }
    }
    arguments.length < 3 && (n = 0);
    return d3.range(e).map(function() {
        var e = [], i;
        for (i = 0; i < t; i++) e[i] = n + n * Math.random();
        for (i = 0; i < 5; i++) r(e);
        return e.map(stream_index);
    });
}

function stream_waves(e, t) {
    return d3.range(e).map(function(e) {
        return d3.range(t).map(function(n) {
            var r = 20 * n / t - e / 3;
            return 2 * r * Math.exp(-0.5 * r);
        }).map(stream_index);
    });
}

function stream_index(e, t) {
    return {
        x: t,
        y: Math.max(0, e)
    };
}

var dataOne = [ {
    key: "Quantity",
    bar: !0,
    values: [ [ 11360052e5, 1271e3 ], [ 11386836e5, 1271e3 ], [ 11411028e5, 1271e3 ], [ 11437812e5, 0 ], [ 11463696e5, 0 ], [ 1149048e6, 0 ], [ 115164e7, 0 ], [ 11543184e5, 0 ], [ 11569968e5, 0 ], [ 11595888e5, 3899486 ], [ 11622708e5, 3899486 ], [ 11648628e5, 3899486 ], [ 11675412e5, 3564700 ], [ 11702196e5, 3564700 ], [ 11726388e5, 3564700 ], [ 11753136e5, 2648493 ], [ 11779056e5, 2648493 ], [ 1180584e6, 2648493 ], [ 1183176e6, 2522993 ], [ 11858544e5, 2522993 ], [ 11885328e5, 2522993 ], [ 11911248e5, 2906501 ], [ 11938032e5, 2906501 ], [ 11963988e5, 2906501 ], [ 11990772e5, 2206761 ], [ 12017556e5, 2206761 ], [ 12042612e5, 2206761 ], [ 1206936e6, 2287726 ], [ 1209528e6, 2287726 ], [ 12122064e5, 2287726 ], [ 12147984e5, 2732646 ], [ 12174768e5, 2732646 ], [ 12201552e5, 2732646 ], [ 12227472e5, 2599196 ], [ 12254256e5, 2599196 ], [ 12280212e5, 2599196 ], [ 12306996e5, 1924387 ], [ 1233378e6, 1924387 ], [ 12357972e5, 1924387 ], [ 1238472e6, 1756311 ], [ 1241064e6, 1756311 ], [ 12437424e5, 1756311 ], [ 12463344e5, 1743470 ], [ 12490128e5, 1743470 ], [ 12516912e5, 1743470 ], [ 12542832e5, 1519010 ], [ 12569616e5, 1519010 ], [ 12595572e5, 1519010 ], [ 12622356e5, 1591444 ], [ 1264914e6, 1591444 ], [ 12673332e5, 1591444 ], [ 1270008e6, 1543784 ], [ 12726e8, 1543784 ], [ 12752784e5, 1543784 ], [ 12778704e5, 1309915 ], [ 12805488e5, 1309915 ], [ 12832272e5, 1309915 ], [ 12858192e5, 1331875 ], [ 12884976e5, 1331875 ], [ 12910932e5, 1331875 ], [ 12937716e5, 1331875 ], [ 129645e7, 1154695 ], [ 12988692e5, 1154695 ], [ 1301544e6, 1194025 ], [ 1304136e6, 1194025 ], [ 13068144e5, 1194025 ], [ 13094064e5, 1194025 ], [ 13120848e5, 1194025 ], [ 13147632e5, 1244525 ], [ 13173552e5, 475e3 ], [ 13200336e5, 475e3 ], [ 13226292e5, 475e3 ], [ 13253076e5, 690033 ], [ 1327986e6, 690033 ], [ 13304916e5, 690033 ], [ 13331664e5, 514733 ], [ 13357584e5, 514733 ] ]
}, {
    key: "Price",
    values: [ [ 11360052e5, 71.89 ], [ 11386836e5, 75.51 ], [ 11411028e5, 68.49 ], [ 11437812e5, 62.72 ], [ 11463696e5, 70.39 ], [ 1149048e6, 59.77 ], [ 115164e7, 57.27 ], [ 11543184e5, 67.96 ], [ 11569968e5, 67.85 ], [ 11595888e5, 76.98 ], [ 11622708e5, 81.08 ], [ 11648628e5, 91.66 ], [ 11675412e5, 84.84 ], [ 11702196e5, 85.73 ], [ 11726388e5, 84.61 ], [ 11753136e5, 92.91 ], [ 11779056e5, 99.8 ], [ 1180584e6, 121.191 ], [ 1183176e6, 122.04 ], [ 11858544e5, 131.76 ], [ 11885328e5, 138.48 ], [ 11911248e5, 153.47 ], [ 11938032e5, 189.95 ], [ 11963988e5, 182.22 ], [ 11990772e5, 198.08 ], [ 12017556e5, 135.36 ], [ 12042612e5, 125.02 ], [ 1206936e6, 143.5 ], [ 1209528e6, 173.95 ], [ 12122064e5, 188.75 ], [ 12147984e5, 167.44 ], [ 12174768e5, 158.95 ], [ 12201552e5, 169.53 ], [ 12227472e5, 113.66 ], [ 12254256e5, 107.59 ], [ 12280212e5, 92.67 ], [ 12306996e5, 85.35 ], [ 1233378e6, 90.13 ], [ 12357972e5, 89.31 ], [ 1238472e6, 105.12 ], [ 1241064e6, 125.83 ], [ 12437424e5, 135.81 ], [ 12463344e5, 142.43 ], [ 12490128e5, 163.39 ], [ 12516912e5, 168.21 ], [ 12542832e5, 185.35 ], [ 12569616e5, 188.5 ], [ 12595572e5, 199.91 ], [ 12622356e5, 210.732 ], [ 1264914e6, 192.063 ], [ 12673332e5, 204.62 ], [ 1270008e6, 235 ], [ 12726e8, 261.09 ], [ 12752784e5, 256.88 ], [ 12778704e5, 251.53 ], [ 12805488e5, 257.25 ], [ 12832272e5, 243.1 ], [ 12858192e5, 283.75 ], [ 12884976e5, 300.98 ], [ 12910932e5, 311.15 ], [ 12937716e5, 322.56 ], [ 129645e7, 339.32 ], [ 12988692e5, 353.21 ], [ 1301544e6, 348.5075 ], [ 1304136e6, 350.13 ], [ 13068144e5, 347.83 ], [ 13094064e5, 335.67 ], [ 13120848e5, 390.48 ], [ 13147632e5, 384.83 ], [ 13173552e5, 381.32 ], [ 13200336e5, 404.78 ], [ 13226292e5, 382.2 ], [ 13253076e5, 405 ], [ 1327986e6, 456.48 ], [ 13304916e5, 542.44 ], [ 13331664e5, 599.55 ], [ 13357584e5, 583.98 ] ]
} ].map(function(e) {
    e.values = e.values.map(function(e) {
        return {
            x: e[0],
            y: e[1]
        };
    });
    return e;
}), chart;

nv.addGraph(function() {
    chart = nv.models.linePlusBarChart().margin({
        top: 30,
        right: 60,
        bottom: 50,
        left: 70
    }).x(function(e, t) {
        return t;
    }).color(d3.scale.category10().range());
    chart.xAxis.tickFormat(function(e) {
        var t = dataOne[0].values[e] && dataOne[0].values[e].x || 0;
        return t ? d3.time.format("%x")(new Date(t)) : "";
    }).showMaxMin(!1);
    chart.y1Axis.tickFormat(d3.format(",f"));
    chart.y2Axis.tickFormat(function(e) {
        return "$" + d3.format(",.2f")(e);
    });
    chart.bars.forceY([ 0 ]).padData(!1);
    d3.select("#line-bar svg").datum(dataOne).transition().duration(500).call(chart);
    nv.utils.windowResize(chart.update);
    chart.dispatch.on("stateChange", function(e) {
        nv.log("New State:", JSON.stringify(e));
    });
    return chart;
});



var chart;
var dataTwo = [{
	key: "Stream 1",
	color: "#FF9800",
	values: [
		{x: 1, y: 1}
	]
}];
nv.addGraph(function() {
  
  chart = nv.models.historicalBarChart();

  chart
      .x(function(d,i) { return d.x });

  chart.xAxis // chart sub-models (ie. xAxis, yAxis, etc) when accessed directly, return themselves, not the parent chart, so need to chain separately
      .tickFormat(d3.format(',.1f'))
      .axisLabel("Time")
      ;

  chart.yAxis
      .axisLabel('Random Number')
      .tickFormat(d3.format(',.4f'));

  chart.showXAxis(true).showYAxis(true).rightAlignYAxis(true).margin({right: 90});

  d3.select('.realtime-bar svg')
      .datum(dataTwo)
      .transition().duration(500)
      .call(chart);

  nv.utils.windowResize(chart.update);

  return chart;
});

var x = 2;
var run = true;
setInterval(function(){
	if (!run) return;
	
	var spike = (Math.random() > 0.95) ? 10: 1;
	dataTwo[0].values.push({
		x: x,
		y: Math.random() * spike
	});

	if (dataTwo[0].values.length > 70) {
		dataTwo[0].values.shift();
	}
	x++;

	chart.update();
}, 500);

d3.select("#start-stop-button").on("click",function() {
	if($(this).hasClass('btn-danger')){
		$(this).removeClass('btn-danger').addClass('btn-info').html('Start Live Stream');
	}
	else
	{
		$(this).removeClass('btn-info').addClass('btn-danger').html('Stop Live Stream');
		
	}
	run = !run;
});

var graph;

nv.addGraph(function() {
    graph = nv.models.scatterChart().showDistX(!0).showDistY(!0).useVoronoi(!0).color(d3.scale.category10().range()).transitionDuration(300);
    graph.xAxis.tickFormat(d3.format(".02f"));
    graph.yAxis.tickFormat(d3.format(".02f"));
    graph.tooltipContent(function(e) {
        return "<h2>" + e + "</h2>";
    });
    d3.select("#bubble svg").datum(randomData(4, 40)).call(graph);
    nv.utils.windowResize(graph.update);
    graph.dispatch.on("stateChange", function(e) {
        "New State:", JSON.stringify(e);
    });
    return graph;
});

var test_data = stream_layers(3, 10 + Math.random() * 100, .1).map(function(e, t) {
    return {
        key: "Stream" + t,
        values: e
    };
});

console.log("td", test_data);


var negative_test_data = (new d3.range(0, 3)).map(function(e, t) {
    return {
        key: "Stream" + t,
        values: (new d3.range(0, 11)).map(function(e, t) {
            return {
                y: 10 + Math.random() * 100 * (Math.floor(Math.random() * 100) % 2 ? 1 : -1),
                x: t
            };
        })
    };
}), chartSub;


nv.addGraph(function() {
    chartSub = nv.models.multiBarChart().barColor(d3.scale.category20().range()).margin({
        bottom: 100
    }).transitionDuration(300).delay(0).rotateLabels(45).groupSpacing(.1);
    chartSub.multibar.hideable(!0);
    chartSub.reduceXTicks(!1).staggerLabels(!0);
    chartSub.xAxis.axisLabel("Current Index").showMaxMin(!0).tickFormat(d3.format(",.6f"));
    chartSub.yAxis.tickFormat(d3.format(",.1f"));
    d3.select("#double-head svg").datum(negative_test_data).call(chartSub);
    nv.utils.windowResize(chartSub.update);
    chartSub.dispatch.on("stateChange", function(e) {
        nv.log("New State:", JSON.stringify(e));
    });
    return chartSub;
});


/*............simple line chart...........................*/

nv.addGraph(function() {
    var chart = nv.models.lineChart()
            .margin({left: 100})  //Adjust chart margins to give the x-axis some breathing room.
            .useInteractiveGuideline(true)  //We want nice looking tooltips and a guideline!
            .transitionDuration(350)  //how fast do you want the lines to transition?
            .showLegend(true)       //Show the legend, allowing users to turn on/off line series.
            .showYAxis(true)        //Show the y-axis
            .showXAxis(true)        //Show the x-axis
        ;

    chart.xAxis     //Chart x-axis settings
        .axisLabel('Time (ms)')
        .tickFormat(d3.format(',r'));

    chart.yAxis     //Chart y-axis settings
        .axisLabel('Voltage (v)')
        .tickFormat(d3.format('.02f'));

    /* Done setting the chart up? Time to render it!*/
    var myData = sinAndCos();   //You need data...

    d3.select('#chart-line svg')    //Select the <svg> element you want to render the chart in.
        .datum(myData)         //Populate the <svg> element with chart data...
        .call(chart);          //Finally, render the chart!

    //Update the chart when window resizes.
    nv.utils.windowResize(function() { chart.update() });
    return chart;
});
/**************************************
 * Simple test data generator
 */
function sinAndCos() {
    var sin = [],sin2 = [],
        cos = [];

    //Data is represented as an array of {x,y} pairs.
    for (var i = 0; i < 100; i++) {
        sin.push({x: i, y: Math.sin(i/10)});
        sin2.push({x: i, y: Math.sin(i/10) *0.25 + 0.5});
        cos.push({x: i, y: .5 * Math.cos(i/10)});
    }

    //Line chart data should be sent as an array of series objects.
    return [
        {
            values: sin,      //values - represents the array of {x,y} data points
            key: 'Sine Wave', //key  - the name of the series.
            color: '#ff7f0e'  //color - optional: choose your own line color.
        },
        {
            values: cos,
            key: 'Cosine Wave',
            color: '#2ca02c'
        },
        {
            values: sin2,
            key: 'Another sine wave',
            color: '#7777ff',
            area: true      //area - set to true if you want this line to turn into a filled area chart.
        }
    ];
}
/*...........end.simple line chart...........................*/

/*...............Line with View Finder Chart.................*/

nv.addGraph(function() {
    var chart = nv.models.lineWithFocusChart();

    chart.xAxis
        .tickFormat(d3.format(',f'));

    chart.yAxis
        .tickFormat(d3.format(',.2f'));

    chart.y2Axis
        .tickFormat(d3.format(',.2f'));

    d3.select('#view-finder svg')
        .datum(testData())
        .transition().duration(500)
        .call(chart);

    nv.utils.windowResize(chart.update);

    return chart;
});
/**************************************
 * Simple test data generator
 */

function testData() {
    return stream_layers(3,128,.1).map(function(data, i) {
        return {
            key: 'Stream' + i,
            values: data
        };
    });
}
/*..............End.Line with View Finder Chart..............*/

/*...............Pie & Donut Chart.................*/

//Regular pie chart example
nv.addGraph(function() {
    var chart = nv.models.pieChart()
        .x(function(d) { return d.label })
        .y(function(d) { return d.value })
        .showLabels(true);

    d3.select("#pie_chart svg")
        .datum(exampleData())
        .transition().duration(350)
        .call(chart);

    return chart;
});

//Donut chart example
nv.addGraph(function() {
    var chart = nv.models.pieChart()
            .x(function(d) { return d.label })
            .y(function(d) { return d.value })
            .showLabels(true)     //Display pie labels
            .labelThreshold(.05)  //Configure the minimum slice size for labels to show up
            .labelType("percent") //Configure what type of data to show in the label. Can be "key", "value" or "percent"
            .donut(true)          //Turn on Donut mode. Makes pie chart look tasty!
            .donutRatio(0.35)     //Configure how big you want the donut hole size to be.
        ;

    d3.select("#donut_chart svg")
        .datum(exampleData())
        .transition().duration(350)
        .call(chart);

    return chart;
});

//Pie chart example data. Note how there is only a single array of key-value pairs.
function exampleData() {
    return  [
        {
            "label": "One",
            "value" : 29.765957771107
        } ,
        {
            "label": "Two",
            "value" : 0
        } ,
        {
            "label": "Three",
            "value" : 32.807804682612
        } ,
        {
            "label": "Four",
            "value" : 196.45946739256
        } ,
        {
            "label": "Five",
            "value" : 0.19434030906893
        } ,
        {
            "label": "Six",
            "value" : 98.079782601442
        } ,
        {
            "label": "Seven",
            "value" : 13.925743130903
        } ,
        {
            "label": "Eight",
            "value" : 5.1387322875705
        }
    ];
}

